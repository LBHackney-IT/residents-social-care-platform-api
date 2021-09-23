using System.Collections.Generic;
using AutoFixture;
using Bogus;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using Address = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.Address;
using Person = ResidentsSocialCarePlatformApi.V1.Infrastructure.Person;

#nullable enable
namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    public static class E2ETestHelpers
    {
        public static ResidentInformation AddPersonWithRelatedEntitiesToDb(SocialCareContext context, int? id = null,
            string? firstname = null, string? lastname = null, string? postcode = null, string? addressLines = null)
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
            var fixture = new Fixture();
            var faker = new Faker();

            var noteTypeCode = fixture.Create<NoteType>().Type;
            var noteTypeDescription = fixture.Create<NoteType>().Description;
            var savedNoteType = TestHelper.CreateDatabaseNoteType(noteTypeCode, noteTypeDescription);
            context.NoteTypes.Add(savedNoteType);
            var savedWorker = TestHelper.CreateDatabaseWorker();
            context.Workers.Add(savedWorker);


            var caseNoteId = faker.Random.Long(min: 1);
            var savedCaseNote = TestHelper.CreateDatabaseCaseNote(caseNoteId, personId, savedNoteType.Type, savedWorker);


            context.CaseNotes.Add(savedCaseNote);
            context.SaveChanges();

            return new CaseNoteInformation
            {
                MosaicId = savedCaseNote.PersonId.ToString(),
                CaseNoteId = savedCaseNote.Id.ToString(),
                NoteType = savedNoteType.Description,
                CaseNoteTitle = savedCaseNote.Title,
                CreatedOn = savedCaseNote.CreatedOn?.ToString("s"),
                CreatedByName = $"{savedWorker.FirstNames} {savedWorker.LastNames}",
                CreatedByEmail = savedWorker.EmailAddress
            };
        }

        public static CaseNoteInformation AddCaseNoteWithNoteTypeAndWorkerToDatabase(SocialCareContext socialCareContext)
        {
            var noteType = TestHelper.CreateDatabaseNoteType();
            var worker = TestHelper.CreateDatabaseWorker();
            var caseNote = TestHelper.CreateDatabaseCaseNote(noteType: noteType.Type);

            socialCareContext.NoteTypes.Add(noteType);
            socialCareContext.Workers.Add(worker);
            socialCareContext.CaseNotes.Add(caseNote);
            socialCareContext.SaveChanges();

            return new CaseNoteInformation
            {
                MosaicId = caseNote.PersonId.ToString(),
                CaseNoteTitle = caseNote.Title,
                CreatedOn = caseNote.CreatedOn?.ToString("s"),
                NoteType = noteType.Description,
                CreatedByName = "Bow Archer",
                CreatedByEmail = worker.EmailAddress,
                CaseNoteContent = caseNote.Note,
                CaseNoteId = caseNote.Id.ToString()
            };
        }

        public static Visit AddVisitToDatabase(SocialCareContext socialCareContext, long? workerId = null)
        {
            var visitInformation = TestHelper.CreateDatabaseVisit(workerId: workerId);
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
