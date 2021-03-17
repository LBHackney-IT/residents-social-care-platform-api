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
    public class SocialCareControllerTests
    {
        private SocialCareController _classUnderTest;
        private Mock<IGetAllResidentsUseCase> _mockGetAllResidentsUseCase;
        private Mock<IGetEntityByIdUseCase> _mockGetEntityByIdUseCase;

        private Mock<IGetAllCaseNotesUseCase> _mockGetAllCaseNotesUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockGetAllResidentsUseCase = new Mock<IGetAllResidentsUseCase>();
            _mockGetEntityByIdUseCase = new Mock<IGetEntityByIdUseCase>();

            _mockGetAllCaseNotesUseCase = new Mock<IGetAllCaseNotesUseCase>();

            _classUnderTest = new SocialCareController(_mockGetAllResidentsUseCase.Object,
                _mockGetEntityByIdUseCase.Object, _mockGetAllCaseNotesUseCase.Object);
        }

        [Test]
        public void ViewingASingleResidentRecord()
        {
            var residentInfo = new ResidentInformation
            {
                MosaicId = "abc123",
                FirstName = "test",
                LastName = "test",
                DateOfBirth = "01/01/2020"
            };

            _mockGetEntityByIdUseCase.Setup(x => x.Execute(12345)).Returns(residentInfo);
            var response = _classUnderTest.ViewRecord(12345) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentInfo);
        }

        [Test]
        public void ListingResidentInformationRecords()
        {
            var residentInfo = new List<ResidentInformation>
            {
                new ResidentInformation()
                {
                    MosaicId = "abc123",
                    FirstName = "test",
                    LastName = "test",
                    DateOfBirth = "01/01/2020"
                }
            };

            var residentInformationList = new ResidentInformationList()
            {
                Residents = residentInfo
            };

            var rqp = new ResidentQueryParam
            {
                FirstName = "Ciasom",
                LastName = "Tessellate",
            };

            _mockGetAllResidentsUseCase.Setup(x => x.Execute(rqp, 3, 2)).Returns(residentInformationList);
            var response = _classUnderTest.ListContacts(rqp, 3, 2) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentInformationList);
        }

        [Test]
        public void ListingCaseNoteInformationRecords()
        {
            var fakeTime = new DateTime();

            var caseNoteInfo = new List<CaseNoteInformation>
            {
                new CaseNoteInformation
                {
                    MosaicId = "00000",
                    CaseNoteId = 2222,
                    CaseNoteTitle = "THIS CASE NOTE HAS A TITLE",
                    EffectiveDate = fakeTime.ToString("s"),
                    CreatedOn = fakeTime.ToString("s"),
                    LastUpdatedOn = fakeTime.ToString("s")
                }
            };

            var caseNoteInformationList = new CaseNoteInformationList
            {
                CaseNotes = caseNoteInfo
            };

            _mockGetAllCaseNotesUseCase.Setup(x => x.Execute(00000)).Returns(caseNoteInformationList);
            var response = _classUnderTest.ListCaseNotes(00000) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(caseNoteInformationList);
        }

    }
}
