using System;
using Microsoft.EntityFrameworkCore;
using MosaicResidentInformationApi.V1.Infrastructure;
using NUnit.Framework;

namespace MosaicResidentInformationApi.Tests
{
    public class DatabaseTests
    {
        protected MosaicContext _mosaicContext;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new DbContextOptionsBuilder();

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                            @"Host=test-database;Port=5432;Database=entitycorex;Username=postgres;Password=mypassword";

            builder.UseNpgsql(connectionString);

            _mosaicContext = new MosaicContext(builder.Options);

            _mosaicContext.Database.EnsureCreated();

            _mosaicContext.Database.BeginTransaction();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            _mosaicContext.Database.RollbackTransaction();
        }
    }
}
