using System.Collections.Generic;
using AutoFixture;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using Address = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.Address;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    public static class E2ETestHelpers
    {
        public static ResidentInformation AddPersonWithRelatedEntitiesToDb(SocialCareContext context, int? id = null, string firstname = null, string lastname = null, string postcode = null, string addressLines = null)
        {
            var person = TestHelper.CreateDatabasePersonEntity(id, firstname, lastname);
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
                DateOfBirth = person.DateOfBirth?.ToString("s"),
                AgeContext = person.AgeContext,
                AddressList = new List<Address>
                {
                    new Address {AddressLine1 = address.AddressLines, PostCode = address.PostCode}
                },
                Restricted = person.Restricted
            };
        }

        public static Person AddPersonToDatabase(SocialCareContext context)
        {
            var person = TestHelper.CreateDatabasePersonEntity();
            context.Persons.Add(person);
            context.SaveChanges();

            return person;
        }

        public static CaseNoteInformation AddCaseNoteForASpecificPersonToDb(SocialCareContext context, long personId)
        {
            var faker = new Fixture();

            var noteTypeCode = faker.Create<NoteType>().Type;
            var noteTypeDescription = faker.Create<NoteType>().Description;
            var savedNoteType = TestHelper.CreateDatabaseNoteType(noteTypeCode, noteTypeDescription);
            context.NoteTypes.Add(savedNoteType);

            var workerFirstName = faker.Create<Worker>().FirstNames;
            var workerLastName = faker.Create<Worker>().LastNames;
            var workerEmailAddress = faker.Create<Worker>().EmailAddress;
            var workerSystemUserId = faker.Create<string>().Substring(0, 10);
            var savedWorker = TestHelper.CreateDatabaseWorker(workerFirstName, workerLastName, workerEmailAddress, workerSystemUserId);
            context.Workers.Add(savedWorker);

            var caseNoteId = faker.Create<CaseNote>().Id;
            var savedCaseNote = TestHelper.CreateDatabaseCaseNote(caseNoteId, personId, savedNoteType.Type, savedWorker.SystemUserId, savedWorker.SystemUserId, savedWorker.SystemUserId);


            context.CaseNotes.Add(savedCaseNote);
            context.SaveChanges();

            return new CaseNoteInformation
            {
                MosaicId = savedCaseNote.PersonId.ToString(),
                CaseNoteId = savedCaseNote.Id,
                NoteType = savedNoteType.Description,
                CaseNoteTitle = savedCaseNote.Title,
                EffectiveDate = savedCaseNote.EffectiveDate?.ToString("s"),
                CreatedOn = savedCaseNote.CreatedOn?.ToString("s"),
                CreatedByName = $"{workerFirstName} {workerLastName}",
                CreatedByEmail = workerEmailAddress,
                LastUpdatedOn = savedCaseNote.LastUpdatedOn?.ToString("s"),
                LastUpdatedName = $"{workerFirstName} {workerLastName}",
                LastUpdatedEmail = workerEmailAddress,
                CompletedDate = savedCaseNote.CompletedDate?.ToString("s"),
                TimeoutDate = savedCaseNote.TimeoutDate?.ToString("s"),
                CopyOfCaseNoteId = savedCaseNote.CopyOfCaseNoteId,
                CopiedDate = savedCaseNote.CopiedDate?.ToString("s"),
                CopiedByName = $"{workerFirstName} {workerLastName}",
                CopiedByEmail = workerEmailAddress,
                RootCaseNoteId = savedCaseNote.RootCaseNoteId,
                PersonVisitId = savedCaseNote.PersonVisitId
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
                EffectiveDate = caseNote.EffectiveDate?.ToString("s"),
                CreatedOn = caseNote.CreatedOn?.ToString("s"),
                LastUpdatedOn = caseNote.LastUpdatedOn?.ToString("s"),
                PersonVisitId = caseNote.PersonVisitId,
                NoteType = noteType.Description,
                CreatedByName = "Bow Archer",
                CreatedByEmail = worker.EmailAddress,
                LastUpdatedName = "Bow Archer",
                LastUpdatedEmail = worker.EmailAddress,
                CaseNoteContent = caseNote.Note,
                RootCaseNoteId = caseNote.RootCaseNoteId,
                CompletedDate = caseNote.CompletedDate?.ToString("s"),
                TimeoutDate = caseNote.TimeoutDate?.ToString("s"),
                CopyOfCaseNoteId = caseNote.CopyOfCaseNoteId,
                CopiedDate = caseNote.CopiedDate?.ToString("s"),
                CopiedByName = "Bow Archer",
                CopiedByEmail = worker.EmailAddress
            };
        }

        public static Visit AddVisitToDatabase(SocialCareContext socialCareContext, long? workerId = null)
        {
            var visitInformation = TestHelper.CreateDatabaseVisit(workerId: workerId).Item1;
            socialCareContext.Visits.Add(visitInformation);
            socialCareContext.SaveChanges();

            return visitInformation;
        }

        public static Worker AddWorkerToDatabase(SocialCareContext socialCareContext)
        {
            var worker = TestHelper.CreateDatabaseWorker();
            socialCareContext.Workers.Add(worker);
            socialCareContext.SaveChanges();

            return worker;
        }
    }

}
