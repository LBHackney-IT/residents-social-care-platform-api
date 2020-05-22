using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MosaicResidentInformationApi.V1.Infrastructure;
using Npgsql;
using NUnit.Framework;

namespace MosaicResidentInformationApi.Tests
{
    public class E2ETests<TStartup> where TStartup : class
    {
        protected HttpClient Client;
        protected MosaicContext MosaicContext;

        private MockWebApplicationFactory<TStartup> _factory;
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _connection = new NpgsqlConnection(ConnectionString.TestDatabase());
            _connection.Open();
            var npgsqlCommand = _connection.CreateCommand();
            npgsqlCommand.CommandText = "SET deadlock_timeout TO 30";
            npgsqlCommand.ExecuteNonQuery();

            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(ConnectionString.TestDatabase());
            MosaicContext = new MosaicContext(builder.Options);
            MosaicContext.Database.EnsureCreated();
        }

        [SetUp]
        public void BaseSetup()
        {
            _factory = new MockWebApplicationFactory<TStartup>(_connection);
            Client = _factory.CreateClient();

            _transaction = _connection.BeginTransaction();
        }

        [TearDown]
        public void BaseTearDown()
        {
            Client.Dispose();
            _factory.Dispose();
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}