using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.UseCase;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    public class GetResidentRecordsUseCaseTests
    {
        private Mock<IGetVisitInformationByPersonId> _mockGetVisitInformationByPersonId = null!;
        private Mock<IGetAllCaseNotesUseCase> _mockGetAllCaseNotesUseCase = null!;
        private GetResidentRecordsUseCase _classUnderTest = null!;

        [SetUp]
        public void SetUp()
        {
            _mockGetVisitInformationByPersonId = new Mock<IGetVisitInformationByPersonId>();
            _mockGetAllCaseNotesUseCase = new Mock<IGetAllCaseNotesUseCase>();
            _classUnderTest = new GetResidentRecordsUseCase(
                _mockGetVisitInformationByPersonId.Object,
                _mockGetAllCaseNotesUseCase.Object);
        }

        [Test]
        public void WhenThereIsNoCaseNotesOrVisits_ReturnEmptyList()
        {
            const long fakePersonId = 123L;
            var visits = new List<VisitInformation>();
            var caseNotes = new CaseNoteInformationList {CaseNotes = new List<CaseNoteInformation>()};
            _mockGetVisitInformationByPersonId.Setup(x => x.Execute(fakePersonId)).Returns(visits);
            _mockGetAllCaseNotesUseCase.Setup(x => x.Execute(fakePersonId)).Returns(caseNotes);

            var response = _classUnderTest.Execute(fakePersonId);

            response.Should().BeEmpty();
        }

        [Test]
        public void WhenThereIsVisits_ReturnListWithVisits()
        {
            const long fakePersonId = 123L;
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain().ToResponse();
            var visits = new List<VisitInformation>{visit};
            var caseNotes = new CaseNoteInformationList {CaseNotes = new List<CaseNoteInformation>()};
            _mockGetVisitInformationByPersonId.Setup(x => x.Execute(fakePersonId)).Returns(visits);
            _mockGetAllCaseNotesUseCase.Setup(x => x.Execute(fakePersonId)).Returns(caseNotes);

            var response = _classUnderTest.Execute(fakePersonId);

            response.Count.Should().Be(visits.Count);
            response.Should().BeEquivalentTo(visit.ToResponse());
        }

        [Test]
        public void WhenThereIsCaseNotes_ReturnListWithVisits()
        {
            const long fakePersonId = 123L;
            var visits = new List<VisitInformation>();
            var caseNote = TestHelper.CreateDatabaseCaseNote().ToDomain().ToResponse();
            var caseNotes = new CaseNoteInformationList {CaseNotes = new List<CaseNoteInformation>{caseNote}};
            _mockGetVisitInformationByPersonId.Setup(x => x.Execute(fakePersonId)).Returns(visits);
            _mockGetAllCaseNotesUseCase.Setup(x => x.Execute(fakePersonId)).Returns(caseNotes);

            var response = _classUnderTest.Execute(fakePersonId);

            response.Count.Should().Be(caseNotes.CaseNotes.Count);
            response.Should().BeEquivalentTo(caseNote.ToResponse());
        }

        [Test]
        public void WhenThereIsCaseNotesAndVisits_ReturnListWithBoth()
        {
            const long fakePersonId = 123L;
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain().ToResponse();
            var visits = new List<VisitInformation>{visit};
            var caseNote = TestHelper.CreateDatabaseCaseNote().ToDomain().ToResponse();
            var caseNotes = new CaseNoteInformationList {CaseNotes = new List<CaseNoteInformation>{caseNote}};
            _mockGetVisitInformationByPersonId.Setup(x => x.Execute(fakePersonId)).Returns(visits);
            _mockGetAllCaseNotesUseCase.Setup(x => x.Execute(fakePersonId)).Returns(caseNotes);

            var response = _classUnderTest.Execute(fakePersonId);

            response.Count.Should().Be(caseNotes.CaseNotes.Count + visits.Count);
            response[0].Should().BeEquivalentTo(visit.ToResponse());
            response[1].Should().BeEquivalentTo(caseNote.ToResponse());
        }
    }
}
