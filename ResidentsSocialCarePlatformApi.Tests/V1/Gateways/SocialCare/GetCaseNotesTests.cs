using System.Linq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways.SocialCare
{
    public class GetCaseNotesTests : DatabaseTests
    {
        private SocialCareGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new SocialCareGateway(SocialCareContext);
        }

        [Test]
        public void WhenThereAreNoMatchingRecords_ReturnsEmptyList()
        {
            var response = _classUnderTest.GetCaseNotes(123);

            response.Should().BeEmpty();
        }

        [Test]
        public void WhenThereIsOneMatch_ReturnsCaseNoteInformationForAGivenPersonId()
        {
            var person = AddPersonToDatabase();
            AddCaseNoteWithNoteTypeAndWorkerToDatabase(person.Id);

            var response = _classUnderTest.GetCaseNotes(person.Id);

            response.Count.Should().Be(1);
            response.FirstOrDefault().Should().NotBe(null);
        }

        [Test]
        public void WhenThereAreMatchingRecords_ReturnsCaseNoteInformationForAGivenPersonId()
        {
            var person = AddPersonToDatabase();
            AddCaseNoteWithNoteTypeAndWorkerToDatabase(person.Id);
            AddCaseNoteWithNoteTypeAndWorkerToDatabase(person.Id, 456);

            var response = _classUnderTest.GetCaseNotes(person.Id);

            response.Count.Should().Be(2);
            response.ElementAt(0).Should().NotBe(null);
            response.ElementAt(1).Should().NotBe(null);
        }

        [Test]
        public void WhenThereAreMatchingRecords_InformationReturnedIsASummaryOfTheCaseNotesForASpecificPersonId()
        {
            var person = AddPersonToDatabase();
            var (caseNote, noteType, caseWorker) = AddCaseNoteWithNoteTypeAndWorkerToDatabase(person.Id);

            var expectedCaseNoteInformation = new CaseNoteInformation
            {
                MosaicId = caseNote.PersonId.ToString(),
                CaseNoteId = caseNote.Id,
                NoteType = noteType.Description,
                CaseNoteTitle = caseNote.Title,
                EffectiveDate = caseNote.EffectiveDate,
                CreatedOn = caseNote.CreatedOn,
                CreatedByName = $"{caseWorker.FirstNames} {caseWorker.LastNames}",
                CreatedByEmail = caseWorker.EmailAddress,
                LastUpdatedOn = caseNote.LastUpdatedOn,
                LastUpdatedName = $"{caseWorker.FirstNames} {caseWorker.LastNames}",
                LastUpdatedEmail = caseWorker.EmailAddress,
                CompletedDate = caseNote.CompletedDate,
                TimeoutDate = caseNote.TimeoutDate,
                CopyOfCaseNoteId = caseNote.CopyOfCaseNoteId,
                CopiedDate = caseNote.CopiedDate,
                CopiedByName = $"{caseWorker.FirstNames} {caseWorker.LastNames}",
                CopiedByEmail = caseWorker.EmailAddress,
                RootCaseNoteId = caseNote.RootCaseNoteId,
                PersonVisitId = caseNote.PersonVisitId
            };

            var response = _classUnderTest.GetCaseNotes(person.Id);

            response.FirstOrDefault().Should().BeEquivalentTo(expectedCaseNoteInformation);
        }

        private Person AddPersonToDatabase(string firstname = null, string lastname = null, int? id = null)
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(firstname, lastname, id);
            SocialCareContext.Persons.Add(databaseEntity);
            SocialCareContext.SaveChanges();
            return databaseEntity;
        }

        private CaseNote AddCaseNoteToDatabase(long personId, long id = 123)
        {
            var caseNote = TestHelper.CreateDatabaseCaseNote(id, personId);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();
            return caseNote;
        }

        private (CaseNote, NoteType, Worker) AddCaseNoteWithNoteTypeAndWorkerToDatabase(long personId, long caseNoteId = 123)
        {
            var faker = new Fixture();

            var caseNoteType = faker.Create<NoteType>().Type;
            var caseNoteTypeDescription = faker.Create<NoteType>().Description;
            var noteType = TestHelper.CreateDatabaseNoteType(caseNoteType, caseNoteTypeDescription);
            SocialCareContext.NoteTypes.Add(noteType);

            var workerFirstNames = faker.Create<Worker>().FirstNames;
            var workerLastNames = faker.Create<Worker>().LastNames;
            var workerEmailAddress = faker.Create<Worker>().EmailAddress;
            var workerSystemUserId = faker.Create<string>().Substring(0, 10);
            var worker = TestHelper.CreateDatabaseWorker(workerFirstNames, workerLastNames, workerEmailAddress, workerSystemUserId);
            SocialCareContext.Workers.Add(worker);

            var caseNote = TestHelper.CreateDatabaseCaseNote(caseNoteId, personId, noteType.Type, workerSystemUserId, workerSystemUserId, workerSystemUserId);
            SocialCareContext.CaseNotes.Add(caseNote);

            SocialCareContext.SaveChanges();

            return (caseNote, noteType, worker);
        }
    }
}
