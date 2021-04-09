using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase;

#nullable enable
namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    public class GetVisitInformationByVisitIdTests
    {
        private Mock<ISocialCareGateway> _mockSocialCareGateway = null!;
        private GetVisitInformationByVisitId _classUnderTest = null!;

        [SetUp]
        public void SetUp()
        {
            _mockSocialCareGateway = new Mock<ISocialCareGateway>();
            _classUnderTest = new GetVisitInformationByVisitId(_mockSocialCareGateway.Object);
        }

        [Test]
        public void WhenThereIsNoMatchingVisit_ReturnsNull()
        {
            VisitInformation? noVisit = null;
            const long fakeVisitId = 123L;
            _mockSocialCareGateway.Setup(x => x.GetVisitInformationByVisitId(fakeVisitId)).Returns(noVisit);

            var response = _classUnderTest.Execute(fakeVisitId);

            response.Should().BeNull();
        }

        [Test]
        public void WhenThereIsAMatchingVisit_ReturnsVisit()
        {
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain();
            _mockSocialCareGateway.Setup(x => x.GetVisitInformationByVisitId(visit.VisitId)).Returns(visit);

            var response = _classUnderTest.Execute(visit.VisitId);

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(visit.ToResponse());
        }
    }
}
