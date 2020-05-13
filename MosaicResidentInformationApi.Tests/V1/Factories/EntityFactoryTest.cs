using FluentAssertions;
using MosaicResidentInformationApi.V1.Factories;
using MosaicResidentInformationApi.V1.Infrastructure;
using NUnit.Framework;

namespace MosaicResidentInformationApi.Tests.V1.Factories
{
    [TestFixture]
    public class EntityFactoryTest
    {
        [Test]
        public void CanBeCreatedFromDatabaseEntity()
        {
            var databaseEntity = new DatabaseEntity();
            var entity = new EntityFactory().ToDomain(databaseEntity);

            entity.Id.Should().Be(databaseEntity.Id);
            entity.CreatedAt.Should().Be(databaseEntity.CreatedAt);
        }
    }
}
