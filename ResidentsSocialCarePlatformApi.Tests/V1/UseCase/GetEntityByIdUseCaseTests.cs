using System;
using AutoFixture;
using FluentAssertions;
using Moq;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase;
using NUnit.Framework;
using ResidentInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;

namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    [TestFixture]
    public class GetEntityByIdCaseTest
    {
        private Mock<ISocialCareGateway> _mockMosaicGateway;
        private GetEntityByIdUseCase _classUnderTest;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockMosaicGateway = new Mock<ISocialCareGateway>();

            _classUnderTest = new GetEntityByIdUseCase(_mockMosaicGateway.Object);
        }

        [Test]
        public void ReturnsResidentInformation()
        {
            var stubbedResidentInfo = _fixture.Create<ResidentInformation>();

            _mockMosaicGateway.Setup(x =>
                    x.GetEntityById(12345, null))
                .Returns(stubbedResidentInfo);

            var response = _classUnderTest.Execute(12345);
            var expectedResponse = stubbedResidentInfo.ToResponse();

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void IfGatewayReturnsNullThrowNotFoundError()
        {
            _mockMosaicGateway.Setup(x => x.GetEntityById(It.IsAny<int>(), null));
            
            Func<ResidentInformationResponse> testDelegate = () => _classUnderTest.Execute(1);

            testDelegate.Should().Throw<ResidentNotFoundException>();
        }
    }
}
