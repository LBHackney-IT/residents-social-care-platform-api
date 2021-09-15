using System;
using AutoFixture;
using Bogus;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using static System.Int32;
using Address = ResidentsSocialCarePlatformApi.V1.Infrastructure.Address;
using Person = ResidentsSocialCarePlatformApi.V1.Infrastructure.Person;

#nullable enable
namespace ResidentsSocialCarePlatformApi.Tests.V1.Helper
{
    public static class TestHelper
    {
        public static Person CreateDatabasePersonEntity(long? id = null, string? firstname = null, string? lastname = null)
        {
            return new Faker<Person>()
                .RuleFor(person => person.Id, f => id ?? f.UniqueIndex)
                .RuleFor(person => person.PersonIdLegacy, f => f.UniqueIndex.ToString())
                .RuleFor(person => person.FirstName, f => firstname ?? f.Name.FirstName())
                .RuleFor(person => person.LastName, f => lastname ?? f.Name.LastName())
                .RuleFor(person => person.FullName, f => f.Name.FullName())
                .RuleFor(person => person.DateOfBirth, f => f.Date.Past(50, DateTime.Now).Date)
                .RuleFor(person => person.NhsNumber, f => f.Random.Number(MaxValue))
                .RuleFor(person => person.Gender, f => f.Person.Gender.ToString()[0].ToString())
                .RuleFor(person => person.EmailAddress, f => f.Person.Email)
                .RuleFor(person => person.Restricted, f => f.Random.String2(1, "YN"))
                .RuleFor(person => person.AgeContext, f => f.Random.String2(1, "YN"))
                .RuleFor(person => person.Title, f => f.Name.Prefix())
                .RuleFor(person => person.Nationality, f => f.Random.String2(1, 20));
        }

        public static Address CreateDatabaseAddressForPersonId(long personId, string? postcode = null, string? address = null)
        {
            var faker = new Fixture();

            var fa = faker.Build<Address>()
                .With(add => add.PersonId, personId)
                .Without(add => add.PersonAddressId)
                .Without(add => add.Person)
                .Without(add => add.EndDate)
                .Without(add => add.ContactAddressFlag)
                .Without(add => add.DisplayAddressFlag)
                .Create();

            fa.PostCode = postcode ?? fa.PostCode;
            fa.AddressLines = address ?? fa.AddressLines;
            return fa;
        }

        public static TelephoneNumber CreateDatabaseTelephoneNumberForPersonId(long personId)
        {
            var faker = new Fixture();

            var random = new Random();
            var possiblePhoneTypes = new[] { "Primary", "Work", "Home", "Pager", "Mobile - Secondary", "Mobile", "Fax", "Ex-directory (do not disclose number)" };
            var randomPhoneTypeIndex = random.Next(0, possiblePhoneTypes.Length);

            return faker.Build<TelephoneNumber>()
                .With(tel => tel.PersonId, personId)
                .With(tel => tel.Type, possiblePhoneTypes[randomPhoneTypeIndex])
                .Without(tel => tel.Id)
                .Without(tel => tel.Person)
                .Create();
        }

        public static CaseNote CreateDatabaseCaseNote(long id = 123, long personId = 123, string noteType = "CASSUMASC",
            string copiedBy = "CGYORFI", string createdBy = "CGYORFI", string updatedBy = "CGYORFI")
        {
            var faker = new Fixture();

            return faker.Build<CaseNote>()
                .With(caseNote => caseNote.Id, id)
                .With(caseNote => caseNote.PersonId, personId)
                .With(caseNote => caseNote.NoteType, noteType)
                .With(caseNote => caseNote.CreatedBy, createdBy)
                .With(caseNote => caseNote.LastUpdatedBy, updatedBy)
                .With(caseNote => caseNote.CopiedBy, copiedBy)
                .Create();
        }

        public static NoteType CreateDatabaseNoteType(string noteType = "CASSUMASC", string description = "Case Summary (ASC)")
        {
            var faker = new Fixture();

            return faker.Build<NoteType>()
                .With(noteTypeI => noteTypeI.Type, noteType)
                .With(noteTypeI => noteTypeI.Description, description)
                .Create();
        }

        public static Worker CreateDatabaseWorker(string firstNames = "Csaba", string lastNames = "Gyorfi", string emailAddress = "cgyorfi@email.com", string systemUserId = "CGYORFI")
        {
            return new Faker<Worker>()
                .RuleFor(worker => worker.Id, f => f.UniqueIndex)
                .RuleFor(worker => worker.FirstNames, firstNames)
                .RuleFor(worker => worker.LastNames, lastNames)
                .RuleFor(worker => worker.EmailAddress, emailAddress)
                .RuleFor(worker => worker.SystemUserId, systemUserId);
        }

        public static Visit CreateDatabaseVisit(
            long? visitId = null,
            long? personId = null,
            long? orgId = null,
            long? workerId = null,
            long? cpVisitScheduleStepId = null,
            long? cpRegistrationId = null)
        {
            Worker worker = CreateDatabaseWorker();

            Visit visit = new Faker<Visit>()
                .RuleFor(v => v.VisitId, f => visitId ?? f.UniqueIndex)
                .RuleFor(v => v.PersonId, f => personId ?? f.UniqueIndex)
                .RuleFor(v => v.VisitType, f => f.Random.String2(1, 20))
                .RuleFor(v => v.PlannedDateTime, f => f.Date.Past())
                .RuleFor(v => v.ActualDateTime, f => f.Date.Past())
                .RuleFor(v => v.ReasonNotPlanned, f => f.Random.String2(1, 16))
                .RuleFor(v => v.ReasonVisitNotMade, f => f.Random.String2(1, 16))
                .RuleFor(v => v.SeenAloneFlag, f => f.Random.String2(1, "YN"))
                .RuleFor(v => v.CompletedFlag, f => f.Random.String2(1, "YN"))
                .RuleFor(v => v.OrgId, f => orgId ?? f.UniqueIndex)
                .RuleFor(v => v.WorkerId, f => workerId ?? worker.Id)
                .RuleFor(v => v.CpRegistrationId, f => cpRegistrationId ?? f.UniqueIndex)
                .RuleFor(v => v.CpVisitScheduleStepId, f => cpVisitScheduleStepId ?? f.UniqueIndex)
                .RuleFor(v => v.CpVisitScheduleDays, f => f.Random.Number(999))
                .RuleFor(v => v.CpVisitOnTime, f => f.Random.String2(1, "YN"))
                .RuleFor(v => v.Worker, worker);

            return visit;
        }

        public static Organisation CreateDatabaseOrganisation(
            long id = 1L,
            string name = "testOrganisationName",
            string responsibleAuthority = "Y"
            )
        {
            var faker = new Fixture();

            return faker.Build<Organisation>()
                .With(organisation => organisation.Id, id)
                .With(organisation => organisation.Name, name)
                .With(organisation => organisation.ResponsibleAuthority, responsibleAuthority)
                .Create();
        }
    }
}
