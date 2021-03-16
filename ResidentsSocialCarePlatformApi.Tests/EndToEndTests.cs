using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests
{
    [NonParallelizable]
    [TestFixture]
    public class EndToEndTests<TStartup> where TStartup : class
    {
        private HttpClient _client;
        private SocialCareContext _socialCareContext;

        private MockWebApplicationFactory<TStartup> _factory;
        private NpgsqlConnection _connection;
        private IDbContextTransaction _transaction;
        private DbContextOptionsBuilder _builder;

        protected HttpClient Client => _client;
        protected SocialCareContext SocialCareContext => _socialCareContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _connection = new NpgsqlConnection(ConnectionString.TestDatabase());
            _connection.Open();
            var npgsqlCommand = _connection.CreateCommand();
            npgsqlCommand.CommandText = "SET deadlock_timeout TO 30";
            npgsqlCommand.ExecuteNonQuery();

            _builder = new DbContextOptionsBuilder();
            _builder.UseNpgsql(_connection);
        }

        [SetUp]
        public void BaseSetup()
        {
            _factory = new MockWebApplicationFactory<TStartup>(_connection);
            _client = _factory.CreateClient();
            _socialCareContext = new SocialCareContext(_builder.Options);
            _socialCareContext.Database.EnsureCreated();
            _transaction = SocialCareContext.Database.BeginTransaction();
        }

        [TearDown]
        public void BaseTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
