using System.Linq;
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
            AddCaseNoteToDatabase(personId: person.Id);

            var response = _classUnderTest.GetCaseNotes(person.Id);

            response.FirstOrDefault().Should().NotBe(null);
        }

        [Test]
        public void WhenThereAreMatchingRecords_ReturnsCaseNoteInformationForAGivenPersonId()
        {
            var person = AddPersonToDatabase();
            AddCaseNoteToDatabase(id: 123, personId: person.Id);
            AddCaseNoteToDatabase(id: 456, personId: person.Id);

            var response = _classUnderTest.GetCaseNotes(person.Id);

            response.Count.Should().Be(2);
            response.ElementAt(0).Should().NotBe(null);
            response.ElementAt(1).Should().NotBe(null);
        }

        [Test]
        public void WhenThereAreMatchingRecords_InformationReturnedIsASummaryOfTheCaseNotesForASpecificPersonId()
        {
            var person = AddPersonToDatabase();
            var caseNote = AddCaseNoteToDatabase(personId: person.Id);

            var expectedCaseNoteInformation = new CaseNoteInformation
            {
                MosaicId = caseNote.PersonId.ToString(),
                CaseNoteId = caseNote.Id,
                CaseNoteTitle = caseNote.Title,
                EffectiveDate = caseNote.EffectiveDate,
                CreatedOn = caseNote.CreatedOn,
                LastUpdatedOn = caseNote.LastUpdatedOn
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
            var caseNote = TestHelper.CreateDatabaseCaseNote(id: id, personId: personId);
            SocialCareContext.CaseNotes.Add(caseNote);
            SocialCareContext.SaveChanges();
            return caseNote;
        }
    }
}
