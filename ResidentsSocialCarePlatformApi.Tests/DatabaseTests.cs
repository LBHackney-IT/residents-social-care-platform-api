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
        }

        [SetUp]
        public void SetUp()
        {
            _context = new SocialCareContext(_builder.Options);
            _context.Database.EnsureCreated();
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
