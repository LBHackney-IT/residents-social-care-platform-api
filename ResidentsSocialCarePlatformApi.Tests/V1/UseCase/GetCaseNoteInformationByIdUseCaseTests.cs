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
    public class GetCaseNoteInformationByIdUseCaseTest
    {
        private Mock<ISocialCareGateway> _mockSocialCareGateway;
        private GetCaseNoteInformationByIdUseCase _classUnderTest;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockSocialCareGateway = new Mock<ISocialCareGateway>();
            _classUnderTest = new GetCaseNoteInformationByIdUseCase(_mockSocialCareGateway.Object);
        }

        [Test]
        public void WhenThereIsNoMatchingCaseNote_ReturnsNull()
        {
            _mockSocialCareGateway.Setup(x => x.GetCaseNoteInformationById(123));

            var response = _classUnderTest.Execute(123);

            response.Should().BeNull();
        }

        [Test]
        public void WhenThereIsAMatchingCaseNote_ReturnsCaseNoteInformation()
        {
            var caseNote = _fixture.Create<CaseNoteInformation>();
            _mockSocialCareGateway.Setup(x => x.GetCaseNoteInformationById(123)).Returns(caseNote);

            var response = _classUnderTest.Execute(123);

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(caseNote.ToResponse());
        }
    }
}
