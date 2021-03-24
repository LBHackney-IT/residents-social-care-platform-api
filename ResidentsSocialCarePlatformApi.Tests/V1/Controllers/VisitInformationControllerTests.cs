using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Controllers;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Controllers
{
    public class VisitInformationControllerTests
    {
        private VisitInformationController _classUnderTest;
        private Mock<IGetVisitInformationByPersonId> _mockGetCaseVisitInformationByPersonIdUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockGetCaseVisitInformationByPersonIdUseCase = new Mock<IGetVisitInformationByPersonId>();

            _classUnderTest = new VisitInformationController(_mockGetCaseVisitInformationByPersonIdUseCase.Object);
        }

        [Test]
        public void GetCaseNote_WhenThereIsAMatchingCaseNoteId_ReturnsCaseNoteInformation()
        {
            var formattedDateTime = new DateTime(2020, 4, 1, 20, 30, 00).ToString("s");
            const long visitId = 12345L;

            var visitInformation = new List<VisitInformation>
            {
                new VisitInformation
                {
                    VisitId = visitId,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = formattedDateTime,
                    ActualDateTime = formattedDateTime,
                    ReasonNotPlanned = "ReasonNotPlanned",
                    ReasonVisitNotMade = "ReasonVisitNotMade",
                    SeenAloneFlag = true,
                    CompletedFlag = true,
                    OrgId = 000L,
                    WorkerId = 000L,
                    CpRegistrationId = 000L,
                    CpVisitScheduleStepId = 000L,
                    CpVisitScheduleDays = 000L,
                    CpVisitOnTime = true
                }
            };

            _mockGetCaseVisitInformationByPersonIdUseCase.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisitInformation(visitId) as OkObjectResult;

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(visitInformation);
        }

        [Test]
        public void GetCaseNote_WhenThereIsNoAMatchingVisits_ReturnEmptyTList()
        {
            var visitInformation = new List<VisitInformation>();
            const long visitId = 12345L;

            _mockGetCaseVisitInformationByPersonIdUseCase.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisitInformation(visitId) as NotFoundResult;

            response.StatusCode.Should().Be(404);
        }
    }
}
