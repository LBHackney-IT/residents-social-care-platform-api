using System.Collections.Generic;
using AutoFixture;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using Address = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.Address;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    public static class E2ETestHelpers
    {
        public static ResidentInformation AddPersonWithRelatedEntitiesToDb(SocialCareContext context, int? id = null, string firstname = null, string lastname = null, string postcode = null, string addressLines = null)
        {
            var person = TestHelper.CreateDatabasePersonEntity(firstname, lastname, id);
            var addedPerson = context.Persons.Add(person);
            context.SaveChanges();

            var address = TestHelper.CreateDatabaseAddressForPersonId(addedPerson.Entity.Id, address: addressLines, postcode: postcode);
            var phone = TestHelper.CreateDatabaseTelephoneNumberForPersonId(addedPerson.Entity.Id);

            context.Addresses.Add(address);
            context.TelephoneNumbers.Add(phone);
            context.SaveChanges();

            return new ResidentInformation
            {
                MosaicId = person.Id.ToString(),
                FirstName = person.FirstName,
                LastName = person.LastName,
                Uprn = address.Uprn.ToString(),
                NhsNumber = person.NhsNumber.ToString(),
                Gender = person.Gender,
                Nationality = person.Nationality,
                PhoneNumber =
                    new List<Phone>
                    {
                        new Phone {PhoneNumber = phone.Number, PhoneType = phone.Type}
                    },
                DateOfBirth = person.DateOfBirth?.ToString("O"),
                AgeContext = person.AgeContext,
                AddressList = new List<Address>
                {
                    new Address {AddressLine1 = address.AddressLines, PostCode = address.PostCode}
                },
                Restricted = person.Restricted
            };
        }

        public static Person AddPersonToDatabase(SocialCareContext context, long personId)
        {
            var person = TestHelper.CreateDatabasePersonEntity(id: personId);
            context.Persons.Add(person);
            context.SaveChanges();

            return person;
        }

        public static CaseNoteInformation AddCaseNoteForASpecificPersonToDb(SocialCareContext context, long personId)
        {
            var faker = new Fixture();
            var caseNoteId = faker.Create<long>();

            var caseNote = TestHelper.CreateDatabaseCaseNote(caseNoteId, personId);

            context.CaseNotes.Add(caseNote);
            context.SaveChanges();

            return new CaseNoteInformation
            {
                MosaicId = personId.ToString(),
                CaseNoteId = caseNote.Id,
                CaseNoteTitle = caseNote.Title,
                EffectiveDate = caseNote.EffectiveDate.ToString("s"),
                CreatedOn = caseNote.CreatedOn.ToString("s"),
                LastUpdatedOn = caseNote.LastUpdatedOn.ToString("s")
            };
        }

        public static CaseNoteInformation AddCaseNoteWithNoteTypeAndWorkerToDatabase(SocialCareContext socialCareContext)
        {
            var noteType = TestHelper.CreateDatabaseNoteType();
            var worker = TestHelper.CreateDatabaseWorker(firstNames: "Bow", lastNames: "Archer");
            var caseNote = TestHelper.CreateDatabaseCaseNote(noteType: noteType.Type, createdBy: worker.SystemUserId, updatedBy: worker.SystemUserId, copiedBy: worker.SystemUserId);

            socialCareContext.NoteTypes.Add(noteType);
            socialCareContext.Workers.Add(worker);
            socialCareContext.CaseNotes.Add(caseNote);
            socialCareContext.SaveChanges();

            return new CaseNoteInformation
            {
                MosaicId = caseNote.PersonId.ToString(),
                CaseNoteId = caseNote.Id,
                CaseNoteTitle = caseNote.Title,
                EffectiveDate = caseNote.EffectiveDate.ToString("s"),
                CreatedOn = caseNote.CreatedOn.ToString("s"),
                LastUpdatedOn = caseNote.LastUpdatedOn.ToString("s"),
                PersonVisitId = caseNote.PersonVisitId,
                NoteType = noteType.Description,
                CreatedByName = "Bow Archer",
                CreatedByEmail = worker.EmailAddress,
                LastUpdatedName = "Bow Archer",
                LastUpdatedEmail = worker.EmailAddress,
                CaseNoteContent = caseNote.Note,
                RootCaseNoteId = caseNote.RootCaseNoteId,
                CompletedDate = caseNote.CompletedDate.ToString("s"),
                TimeoutDate = caseNote.TimeoutDate.ToString("s"),
                CopyOfCaseNoteId = caseNote.CopyOfCaseNoteId,
                CopiedDate = caseNote.CopiedDate.ToString("s"),
                CopiedByName = "Bow Archer",
                CopiedByEmail = worker.EmailAddress,
            };
        }
    }
}
