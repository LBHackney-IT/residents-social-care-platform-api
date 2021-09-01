using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase;

namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    [TestFixture]
    public class GetVisitInformationByPersonIdTest
    {
        private Mock<ISocialCareGateway> _mockSocialCareGateway;
        private GetVisitInformationByPersonId _classUnderTest;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockSocialCareGateway = new Mock<ISocialCareGateway>();
            _classUnderTest = new GetVisitInformationByPersonId(_mockSocialCareGateway.Object);
        }

        [Test]
        public void IfNoMatchingRecords_ReturnsAnEmptyResponse()
        {
            const long fakePersonId = 34567L;

            _mockSocialCareGateway
                .Setup(x => x.GetVisitInformationByPersonId(fakePersonId))
                .Returns(new List<VisitInformation>());

            var response = _classUnderTest.Execute(fakePersonId);

            response.Should().BeEmpty();
        }

        [Test]
        public void ReturnsListOfVisitInformation()
        {
            var visitInformation = _fixture.CreateMany<VisitInformation>().ToList();
            var visitInformationResponse = from visit in visitInformation select visit.ToResponse();
            const long fakePersonId = 34567L;

            _mockSocialCareGateway.Setup(x => x.GetVisitInformationByPersonId(fakePersonId))
                .Returns(visitInformation);

            var response = _classUnderTest.Execute(34567);

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(visitInformationResponse);
        }
    }
}
