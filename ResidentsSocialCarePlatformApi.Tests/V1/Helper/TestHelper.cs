using System;
using AutoFixture;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using Address = ResidentsSocialCarePlatformApi.V1.Infrastructure.Address;
using Person = ResidentsSocialCarePlatformApi.V1.Infrastructure.Person;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Helper
{
    public static class TestHelper
    {
        public static Person CreateDatabasePersonEntity(string firstname = null, string lastname = null, long? id = null)
        {
            var faker = new Fixture();
            var fp = faker.Build<Person>()
                .Without(p => p.Id)
                .Create();
            fp.DateOfBirth = new DateTime
                (fp.DateOfBirth.Value.Year, fp.DateOfBirth.Value.Month, fp.DateOfBirth.Value.Day);
            fp.FirstName = firstname ?? fp.FirstName;
            fp.LastName = lastname ?? fp.LastName;
            if (id != null) fp.Id = (int) id;

            return fp;
        }

        public static Address CreateDatabaseAddressForPersonId(long personId, string postcode = null, string address = null)
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
                .With(noteType => noteType.Type, noteType)
                .With(noteType => noteType.Description, description)
                .Create();
        }

        public static Worker CreateDatabaseWorker(string firstNames = "Csaba", string lastNames = "Gyorfi", string emailAddress = "cgyorfi@email.com", string systemUserId = "CGYORFI")
        {
            var faker = new Fixture();

            return faker.Build<Worker>()
                .With(worker => worker.FirstNames, firstNames)
                .With(worker => worker.LastNames, lastNames)
                .With(worker => worker.EmailAddress, emailAddress)
                .With(worker => worker.SystemUserId, systemUserId)
                .Create();
        }

        public static Visit CreateDatabaseVisit(long visitId = 0, long personId = 0, string visitType = "testVisitType",
            long orgId = 0, long workerId = 0)
        {
            var faker = new Fixture();

            return faker.Build<Visit>()
                .With(visit => visit.VisitId, visitId)
                .With(visit => visit.PersonId, personId)
                .With(visit => visit.VisitType, visitType)
                .With(visit => visit.OrgId, orgId)
                .With(visit => visit.WorkerId, workerId)
                .Create();
        }

        public static Organisation CreateDatabaseOrganisation(long id = 0, string name = "testOrganisationName")
        {
            var faker = new Fixture();

            return faker.Build<Organisation>()
                .With(organisation => organisation.Id, id)
                .With(organisation => organisation.Name, name)
                .Create();
        }
    }
}
