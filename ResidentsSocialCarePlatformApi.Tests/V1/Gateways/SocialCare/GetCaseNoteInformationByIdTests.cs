using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways
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
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase(id: 123);
            AddCaseNoteWithNoteTypeAndWorkerToDatabase(id: 456, caseNoteType: "DIFF", caseNoteTypeDescription: "Different");

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response.CaseNoteId.Should().Be(caseNote.Id);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsDetailsFromCaseNotes()
        {
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase();

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response.MosaicId.Should().Be(caseNote.PersonId.ToString());
            response.CaseNoteId.Should().Be(caseNote.Id);
            response.PersonVisitId.Should().Be(caseNote.PersonVisitId);
            response.CaseNoteTitle.Should().Be(caseNote.Title);
            response.EffectiveDate.Should().Be(caseNote.EffectiveDate);
            response.CreatedOn.Should().Be(caseNote.CreatedOn);
            response.LastUpdatedOn.Should().Be(caseNote.LastUpdatedOn);
            response.CaseNoteContent.Should().Be(caseNote.Note);
            response.RootCaseNoteId.Should().Be(caseNote.RootCaseNoteId);
            response.CompletedDate.Should().Be(caseNote.CompletedDate);
            response.TimeoutDate.Should().Be(caseNote.TimeoutDate);
            response.CopyOfCaseNoteId.Should().Be(caseNote.CopyOfCaseNoteId);
            response.CopiedDate.Should().Be(caseNote.CopiedDate);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsNoteTypeFromNoteTypes()
        {
            const string noteType = "NOTETYPE";
            const string noteTypeDescription = "Note Type Description";
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase(caseNoteType: noteType, caseNoteTypeDescription: noteTypeDescription);

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response.NoteType.Should().Be(noteTypeDescription);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsDetailsFromWorkers()
        {
            const string workerEmailAddress = "adora@grayskull.com";
            var caseNote = AddCaseNoteWithNoteTypeAndWorkerToDatabase(copiedBy: "AGRAYSKULL", workerFirstNames: "Adora", workerLastNames: "Grayskull", workerEmailAddress: workerEmailAddress, workerSystemUserId: "AGRAYSKULL");

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response.CreatedByName.Should().Be("Adora Grayskull");
            response.CreatedByEmail.Should().Be(workerEmailAddress);
            response.LastUpdatedName.Should().Be("Adora Grayskull");
            response.LastUpdatedEmail.Should().Be(workerEmailAddress);
            response.CopiedByName.Should().Be("Adora Grayskull");
            response.CopiedByEmail.Should().Be(workerEmailAddress);
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

            response.NoteType.Should().BeNull();
        }

        [Test]
        public void WhenCopiedByIsNull_ReturnsNullForCopiedByNameAndCopiedByEmail()
        {
            var noteType = TestHelper.CreateDatabaseNoteType();
            var worker = TestHelper.CreateDatabaseWorker();
            var caseNote = TestHelper.CreateDatabaseCaseNote(noteType: noteType.Type, workerSystemUserId: worker.SystemUserId, copiedBy: null);
            SocialCareContext.NoteTypes.Add(noteType);
            SocialCareContext.Workers.Add(worker);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response.CopiedByName.Should().BeNull();
            response.CopiedByEmail.Should().BeNull();
        }

        [Test]
        public void WhenCreatedByWorkerIsNull_ReturnsNullForCreatedByNameAndEmail()
        {
            var noteType = TestHelper.CreateDatabaseNoteType();
            var worker = TestHelper.CreateDatabaseWorker(systemUserId: "existingUser");
            var caseNote = TestHelper.CreateDatabaseCaseNote(noteType: noteType.Type, createdBy: "nonExistingUser");
            SocialCareContext.NoteTypes.Add(noteType);
            SocialCareContext.Workers.Add(worker);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();

            var response = _classUnderTest.GetCaseNoteInformationById(caseNote.Id);

            response.CreatedByEmail.Should().BeNull();
            response.CreatedByName.Should().BeNull();
        }
        private CaseNote AddCaseNoteWithNoteTypeAndWorkerToDatabase(long id = 123, long personId = 123, string caseNoteType = "CASSUMASC", string caseNoteTypeDescription = "Case Summary (ASC)", string copiedBy = "CGYORFI", string workerFirstNames = "Csaba", string workerLastNames = "Gyorfi", string workerEmailAddress = "cgyorfi@email.com", string workerSystemUserId = "CGYORFI")
        {
            var noteType = TestHelper.CreateDatabaseNoteType(caseNoteType, caseNoteTypeDescription);
            var worker = TestHelper.CreateDatabaseWorker(workerFirstNames, workerLastNames, workerEmailAddress, workerSystemUserId);
            var caseNote = TestHelper.CreateDatabaseCaseNote(id, personId, noteType.Type, copiedBy, worker.SystemUserId);

            SocialCareContext.NoteTypes.Add(noteType);
            SocialCareContext.Workers.Add(worker);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();

            return caseNote;
        }
    }
}
