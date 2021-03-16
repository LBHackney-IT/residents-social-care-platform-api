using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Domain.ResidentInformation;
using ResidentInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;

namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    [TestFixture]
    public class GetAllCaseNotesUseCaseTest
    {
        private Mock<ISocialCareGateway> _mockSocialCareGateway;
        private GetAllCaseNotesUseCase _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockSocialCareGateway = new Mock<ISocialCareGateway>();
            _classUnderTest = new GetAllCaseNotesUseCase(_mockSocialCareGateway.Object);
        }

        [Test]
        public void ReturnsListOfSummarisedCaseNoteInformation()
        {
            var stubbedCaseNotes = _fixture.CreateMany<CaseNoteInformation>();

            _mockSocialCareGateway.Setup(x =>
                    x.GetCaseNotes(34567))
                .Returns(stubbedCaseNotes.ToList());

            var response = _classUnderTest.Execute(34567);

            response.Should().NotBeNull();
            response.CaseNotes.Should().BeEquivalentTo(stubbedCaseNotes.ToResponse());
        }
    }
}
