using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Controllers;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;
using NUnit.Framework;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Controllers
{
    [TestFixture]
    public class CaseNotesControllerTests
    {
        private CaseNotesController _classUnderTest;
        private Mock<IGetCaseNoteInformationByIdUseCase> _mockGetCaseNoteInformationByIdUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockGetCaseNoteInformationByIdUseCase = new Mock<IGetCaseNoteInformationByIdUseCase>();

            _classUnderTest = new CaseNotesController(_mockGetCaseNoteInformationByIdUseCase.Object);
        }

        [Test]
        public void GetCaseNote_WhenThereIsAMatchingCaseNoteId_ReturnsCaseNoteInformation()
        {
            const string formattedDateTime = "2021-03-01T15:30:00";
            var caseNoteInformation = new CaseNoteInformation
            {
                MosaicId = "12345",
                CaseNoteId = "67890",
                CaseNoteTitle = "I AM A CASE NOTE",
                CreatedOn = formattedDateTime,
                NoteType = "Case Summary (ASC)",
                CreatedByName = "Finn Grayskull",
                CreatedByEmail = "finn@grayskull.com",
                CaseNoteContent = "I am case note content."
            };
            _mockGetCaseNoteInformationByIdUseCase.Setup(x => x.Execute(67890)).Returns(caseNoteInformation);

            var response = _classUnderTest.GetCaseNote(67890) as OkObjectResult;

            response?.StatusCode.Should().Be(200);
            response?.Value.Should().BeEquivalentTo(caseNoteInformation);
        }

        [Test]
        public void GetCaseNote_WhenThereIsNoAMatchingCaseNoteId_ReturnsNotFound()
        {
            _mockGetCaseNoteInformationByIdUseCase.Setup(x => x.Execute(67890));

            var response = _classUnderTest.GetCaseNote(67890) as NotFoundResult;

            response?.StatusCode.Should().Be(404);
        }
    }
}
