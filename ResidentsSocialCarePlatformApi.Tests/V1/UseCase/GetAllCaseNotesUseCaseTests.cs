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
    public class GetAllCaseNotesUseCaseTest
    {
        private Mock<ISocialCareGateway> _mockSocialCareGateway;
        private GetAllCaseNotesUseCase _classUnderTest;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockSocialCareGateway = new Mock<ISocialCareGateway>();
            _classUnderTest = new GetAllCaseNotesUseCase(_mockSocialCareGateway.Object);
        }

        [Test]
        public void IfNoMatchingRecords_ReturnsAnEmptyResponse()
        {
            var noRecords = new List<CaseNoteInformation>();

            _mockSocialCareGateway.Setup(x =>
                    x.GetAllCaseNotes(34567))
                .Returns(noRecords);

            var response = _classUnderTest.Execute(34567);

            response.CaseNotes.Should().BeEmpty();
        }

        [Test]
        public void ReturnsListOfSummarisedCaseNoteInformation()
        {
            var stubbedCaseNotes = _fixture.CreateMany<CaseNoteInformation>().ToList();

            _mockSocialCareGateway.Setup(x =>
                    x.GetAllCaseNotes(34567))
                .Returns(stubbedCaseNotes);

            var response = _classUnderTest.Execute(34567);

            response.Should().NotBeNull();
            response.CaseNotes.Should().BeEquivalentTo(stubbedCaseNotes.ToResponse());
        }
    }
}
