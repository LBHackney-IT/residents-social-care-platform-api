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
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ResidentsControllerTests
    {
        private ResidentsController _classUnderTest;
        private Mock<IGetAllResidentsUseCase> _mockGetAllResidentsUseCase;
        private Mock<IGetEntityByIdUseCase> _mockGetEntityByIdUseCase;
        private Mock<IGetAllCaseNotesUseCase> _mockGetAllCaseNotesUseCase;
        private Mock<IGetVisitInformationByPersonId> _mockGetVisitInformationByPersonIdUseCase;
        private Mock<IGetResidentRecordsUseCase> _mockGetResidentRecordsUseCase;

        [SetUp]
        public void SetUp()
        {
            _mockGetAllResidentsUseCase = new Mock<IGetAllResidentsUseCase>();
            _mockGetEntityByIdUseCase = new Mock<IGetEntityByIdUseCase>();
            _mockGetAllCaseNotesUseCase = new Mock<IGetAllCaseNotesUseCase>();
            _mockGetVisitInformationByPersonIdUseCase = new Mock<IGetVisitInformationByPersonId>();
            _mockGetResidentRecordsUseCase = new Mock<IGetResidentRecordsUseCase>();

            _classUnderTest = new ResidentsController(
                _mockGetAllResidentsUseCase.Object,
                _mockGetEntityByIdUseCase.Object,
                _mockGetAllCaseNotesUseCase.Object,
                _mockGetVisitInformationByPersonIdUseCase.Object,
                _mockGetResidentRecordsUseCase.Object);
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

        [Test]
        public void GetVisitInformation_WhenThereIsAMatchingVisitId_ReturnsListOfVisitInformationWith200Status()
        {
            const long visitId = 12345L;

            var visitInformation = new List<VisitInformation>
            {
                TestHelper.CreateDatabaseVisit(visitId).Item1.ToDomain().ToResponse()
            };

            _mockGetVisitInformationByPersonIdUseCase.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisitInformation(visitId) as OkObjectResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(visitInformation);
        }

        [Test]
        public void GetVisitInformation_WhenThereIsNoMatchingVisits_Return404Status()
        {
            var visitInformation = new List<VisitInformation>();
            const long visitId = 12345L;

            _mockGetVisitInformationByPersonIdUseCase.Setup(x => x.Execute(visitId)).Returns(visitInformation);

            var response = _classUnderTest.GetVisitInformation(visitId) as NotFoundResult;

            response.StatusCode.Should().Be(404);
        }

        [Test]
        public void GetResidentRecords_WhenThereIsNoMatchingVisitsOrCaseNotes_ReturnEmptyList()
        {
            const long fakerPersonId = 123L;
            var residentRecords = new List<ResidentHistoricRecord>();
            _mockGetResidentRecordsUseCase.Setup(x => x.Execute(fakerPersonId)).Returns(residentRecords);

            var response = _classUnderTest.GetResidentRecords(fakerPersonId) as OkObjectResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(new List<ResidentHistoricRecord>());
        }

        [Test]
        public void GetResidentRecords_WhenThereAreVisits_ReturnListWithVisitData()
        {
            const long fakerPersonId = 123L;
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain().ToResponse().ToSharedResponse(fakerPersonId);
            var residentRecords = new List<ResidentHistoricRecord> {visit};
            _mockGetResidentRecordsUseCase.Setup(x => x.Execute(fakerPersonId)).Returns(residentRecords);

            var response = _classUnderTest.GetResidentRecords(fakerPersonId) as OkObjectResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentRecords);
        }

        [Test]
        public void GetResidentRecords_WhenThereAreCaseNotes_ReturnListWithCaseNoteData()
        {
            const long fakerPersonId = 123L;
            var caseNote = TestHelper.CreateDatabaseCaseNote().ToDomain().ToResponse().ToSharedResponse(fakerPersonId);
            var residentRecords = new List<ResidentHistoricRecord> {caseNote};
            _mockGetResidentRecordsUseCase.Setup(x => x.Execute(fakerPersonId)).Returns(residentRecords);

            var response = _classUnderTest.GetResidentRecords(fakerPersonId) as OkObjectResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentRecords);
        }

        [Test]
        public void GetResidentRecords_WhenThereAreVisitsAndCaseNotes_ReturnListWithVisitAndCaseNoteData()
        {
            const long fakerPersonId = 123L;
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain().ToResponse().ToSharedResponse(fakerPersonId);
            var caseNote = TestHelper.CreateDatabaseCaseNote().ToDomain().ToResponse().ToSharedResponse(fakerPersonId);
            var residentRecords = new List<ResidentHistoricRecord> {visit, caseNote};
            _mockGetResidentRecordsUseCase.Setup(x => x.Execute(fakerPersonId)).Returns(residentRecords);

            var response = _classUnderTest.GetResidentRecords(fakerPersonId) as OkObjectResult;

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentRecords);
        }
    }
}
