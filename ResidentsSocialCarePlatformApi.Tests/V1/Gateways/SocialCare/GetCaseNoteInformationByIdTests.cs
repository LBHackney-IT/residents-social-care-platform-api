using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways.SocialCare
{
    [NonParallelizable]
    [TestFixture]
    public class GetCaseNoteInformationByIdTests : DatabaseTests
    {
        private SocialCareGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new SocialCareGateway(SocialCareContext);
        }

        [Test]
        public void WhenThereIsNoMatchingRecord_ReturnsNull()
        {
            const int existentCaseNoteId = 123;
            const int nonexistentCaseNoteId = 456;
            AddCaseNoteWithNoteTypeAndWorkerToDatabase(id: existentCaseNoteId);

            var response = _classUnderTest.GetCaseNoteInformationById(nonexistentCaseNoteId);

            response.Should().BeNull();
        }

        [Test]
        public void WhenThereAreMultipleCaseNotes_ReturnsCaseNoteWithMatchingId()
        {
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase(id: 32352353);
            AddCaseNoteWithNoteTypeAndWorkerToDatabase(id: 3253253325, caseNoteType: "DIFF", caseNoteTypeDescription: "Different");

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response?.CaseNoteId.Should().Be(caseNote.Id);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsDetailsFromCaseNotes()
        {
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase();

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response?.MosaicId.Should().Be(caseNote.PersonId.ToString());
            response?.CaseNoteId.Should().Be(caseNote.Id);
            response?.CaseNoteTitle.Should().Be(caseNote.Title);
            response?.CreatedOn.Should().Be(caseNote.CreatedOn);
            response?.CaseNoteContent.Should().Be(caseNote.Note);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsNoteTypeFromNoteTypes()
        {
            const string noteType = "NOTETYPE";
            const string noteTypeDescription = "Note Type Description";
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase(caseNoteType: noteType, caseNoteTypeDescription: noteTypeDescription);

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response?.NoteType.Should().Be(noteTypeDescription);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsDetailsFromWorkers()
        {
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase();

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response?.CreatedByName.Should().Be($"{caseNote.Worker.FirstNames} {caseNote.Worker.LastNames}");
            response?.CreatedByEmail.Should().Be(caseNote.Worker.EmailAddress);
        }

        [Test]
        public void WhenNoteTypeCannotBeFound_ReturnsNullForNoteType()
        {
            const string nonexistentNoteType = "NONEXISTENT";
            var existentNoteType = TestHelper.CreateDatabaseNoteType("EXISTENT", "Existent Case Note Type");
            var worker = TestHelper.CreateDatabaseWorker();
            var caseNote = TestHelper.CreateDatabaseCaseNote(noteType: nonexistentNoteType);
            SocialCareContext.NoteTypes.Add(existentNoteType);
            SocialCareContext.Workers.Add(worker);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response?.NoteType.Should().BeNull();
        }

        private CaseNote AddCaseNoteWithNoteTypeAndWorkerToDatabase(long id = 123, long personId = 123, string caseNoteType = "CASSUMASC", string caseNoteTypeDescription = "Case Summary (ASC)", string copiedBy = "CGYORFI", string workerFirstNames = "Csaba", string workerLastNames = "Gyorfi", string workerEmailAddress = "cgyorfi@email.com", string workerSystemUserId = "CGYORFI")
        {
            var noteType = TestHelper.CreateDatabaseNoteType(caseNoteType, caseNoteTypeDescription);
            var worker = TestHelper.CreateDatabaseWorker();
            var caseNote = TestHelper.CreateDatabaseCaseNote(id, personId, noteType.Type, worker);

            SocialCareContext.NoteTypes.Add(noteType);
            SocialCareContext.Workers.Add(worker);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();

            return caseNote;
        }
    }
}
