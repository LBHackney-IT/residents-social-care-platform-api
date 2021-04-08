using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Controllers;
using ResidentsSocialCarePlatformApi.V1.Factories;
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
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain().ToResponse();

            _mockGetVisitInformationByVisitId.Setup(x => x.Execute(visit.VisitId)).Returns(visit);

            var response = _classUnderTest.GetVisit(visit.VisitId) as OkObjectResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(visit);
        }

        [Test]
        public void GetVisitInformation_WhenThereIsNoAMatchingVisitId_ReturnsNotFound()
        {
            VisitInformation visitInformation = null;
            const long visitId = 12345L;
            _mockGetVisitInformationByVisitId.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisit(visitId) as NotFoundResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(404);
        }
    }
}
