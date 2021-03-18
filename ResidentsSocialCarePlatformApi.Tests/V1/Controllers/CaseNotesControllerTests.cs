using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Controllers;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;
using NUnit.Framework;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;

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
                CaseNoteId = 67890,
                CaseNoteTitle = "I AM A CASE NOTE",
                EffectiveDate = formattedDateTime,
                CreatedOn = formattedDateTime,
                LastUpdatedOn = formattedDateTime,
                PersonVisitId = 456,
                NoteType = "Case Summary (ASC)",
                CreatedByName = "Finn Grayskull",
                CreatedByEmail = "finn@grayskull.com",
                LastUpdatedName = "Finn Grayskull",
                LastUpdatedEmail = "finn@grayskull.com",
                CaseNoteContent = "I am case note content.",
                RootCaseNoteId = 789,
                CompletedDate = formattedDateTime,
                TimeoutDate = formattedDateTime,
                CopyOfCaseNoteId = 567,
                CopiedDate = formattedDateTime,
                CopiedByName = "Finn Grayskull",
                CopiedByEmail = "finn@grayskull.com",
            };
            _mockGetCaseNoteInformationByIdUseCase.Setup(x => x.Execute(67890)).Returns(caseNoteInformation);

            var response = _classUnderTest.GetCaseNote(67890) as OkObjectResult;

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(caseNoteInformation);
        }

        [Test]
        public void GetCaseNote_WhenThereIsNoAMatchingCaseNoteId_ReturnsNotFound()
        {
            CaseNoteInformation caseNoteNotFound = null;
            _mockGetCaseNoteInformationByIdUseCase.Setup(x => x.Execute(67890)).Returns(caseNoteNotFound);

            var response = _classUnderTest.GetCaseNote(67890) as NotFoundResult;

            response.StatusCode.Should().Be(404);
        }
    }
}
