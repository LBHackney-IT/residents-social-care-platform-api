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

            MosaicContext.Add(databaseEntity);
            MosaicContext.SaveChanges();

            var result = MosaicContext.Persons.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(databaseEntity);
        }
    }
}
