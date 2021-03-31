using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Controllers;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Controllers
{
    [TestFixture]
    public class VisitsControllerTests
    {
        private VisitInformationController _classUnderTest;
        private Mock<IGetVisitInformationByVisitId> _mockGetVisitInformationByVisitId;

        [SetUp]
        public void SetUp()
        {
            _mockGetVisitInformationByVisitId = new Mock<IGetVisitInformationByVisitId>();

            _classUnderTest = new VisitInformationController(_mockGetVisitInformationByVisitId.Object);
        }

        [Test]
        public void GetVisitInformation_WhenThereIsAMatchingVisitId_ReturnsVisitInformation()
        {
            const string formattedDateTime = "2021-03-01T15:30:00";
            const long visitId = 12345L;
            var visitInformation = new VisitInformation
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
            };
            _mockGetVisitInformationByVisitId.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisit(visitId) as OkObjectResult;

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(visitInformation);
        }

        [Test]
        public void GetVisitInformation_WhenThereIsNoAMatchingVisitId_ReturnsNotFound()
        {
            VisitInformation visitInformation = null;
            const long visitId = 12345L;
            _mockGetVisitInformationByVisitId.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisit(visitId) as NotFoundResult;

            response.StatusCode.Should().Be(404);
        }
    }
}
