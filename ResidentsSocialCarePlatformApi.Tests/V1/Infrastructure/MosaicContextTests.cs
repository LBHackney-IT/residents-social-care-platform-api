using System.Linq;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using NUnit.Framework;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Infrastructure
{
    [TestFixture]
    public class MosaicContextTests : DatabaseTests
    {
        [Test]
        public void CanGetADatabaseEntity()
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity();

            SocialCareContext.Add(databaseEntity);
            SocialCareContext.SaveChanges();

            var result = SocialCareContext.Persons.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(databaseEntity);
        }
    }
}
