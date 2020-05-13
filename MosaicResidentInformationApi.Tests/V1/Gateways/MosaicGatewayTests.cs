using Bogus;
using FluentAssertions;
using MosaicResidentInformationApi.Tests.V1.Helper;
using MosaicResidentInformationApi.V1.Gateways;
using NUnit.Framework;

namespace MosaicResidentInformationApi.Tests.V1.Gateways
{
    [TestFixture]
    public class MosaicGatewayTests : DatabaseTests
    {
        private readonly Faker _faker = new Faker();
        private MosaicGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new MosaicGateway(_mosaicContext);
        }

        [Test]
        public void GetEntityById_ReturnsEmptyArray()
        {
            var response = _classUnderTest.GetEntityById(123);

            response.Should().BeNull();
        }

        [Test]
        public void GetEntityById_ReturnsCorrectResponse()
        {
            var entity = EntityHelper.CreateEntity();
            var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _mosaicContext.DatabaseEntities.Add(databaseEntity);
            _mosaicContext.SaveChanges();

            var response = _classUnderTest.GetEntityById(databaseEntity.Id);

            response.Id.Should().Be(databaseEntity.Id);
            response.CreatedAt.Should().Be(databaseEntity.CreatedAt);
        }
    }
}
