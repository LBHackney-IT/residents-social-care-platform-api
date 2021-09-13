using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests
{
    [NonParallelizable]
    [TestFixture]
    public class DatabaseTests
    {
        private SocialCareContext _context;
        private IDbContextTransaction _transaction;
        private DbContextOptionsBuilder _builder;

        protected SocialCareContext SocialCareContext => _context;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _builder = new DbContextOptionsBuilder();
            _builder.UseNpgsql(ConnectionString.TestDatabase());
            _builder.EnableDetailedErrors();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SocialCareContext(_builder.Options);
            _context.Database.EnsureCreated();

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

            _context.Database.ExecuteSqlRaw(createPersonalRelationshipsSql);

            _transaction = SocialCareContext.Database.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
