using System;
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
            _builder.EnableDetailedErrors();
        }

        [SetUp]
        public void BaseSetup()
        {
            _factory = new MockWebApplicationFactory<TStartup>(_connection);
            _client = _factory.CreateClient();
            _socialCareContext = new SocialCareContext(_builder.Options);
            _socialCareContext.Database.EnsureCreated();

            // Temporary fix until we can get migrations running in our pipeline
            var createPersonalRelationshipsSql = String.Join(
                Environment.NewLine,
                "CREATE OR REPLACE VIEW dbo.vw_personal_relationships AS",
                "SELECT dpr.personal_relationship_id, dpr.person_id, dpr.other_person_id, dprt.description,",
                "CASE",
                "WHEN dprt.family_category = 'Child''s Children' THEN 'children'",
                "WHEN dprt.family_category = 'Child''s Siblings' THEN 'siblings'",
                "WHEN dprt.family_category = 'Child''s Parents' THEN 'parents'",
                "WHEN dprt.family_category = 'Other Family Relationships' THEN 'other'",
                "WHEN dprt.family_category IS NULL THEN 'other'",
                "ELSE 'unknown'",
                "END AS category",
                "FROM",
                "dbo.dm_personal_relationships dpr",
                "INNER JOIN dbo.dm_personal_rel_types dprt ON dpr.personal_rel_type_id = dprt.personal_rel_type_id;");

            _socialCareContext.Database.ExecuteSqlRaw(createPersonalRelationshipsSql);

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
