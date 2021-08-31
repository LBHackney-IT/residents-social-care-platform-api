using System.Collections.Generic;
using System.Linq;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using DbAddress = ResidentsSocialCarePlatformApi.V1.Infrastructure.Address;

namespace ResidentsSocialCarePlatformApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Domain.ResidentInformation ToDomain(this Person databaseEntity)
        {
            return new Domain.ResidentInformation
            {
                MosaicId = databaseEntity.Id.ToString(),
                FirstName = databaseEntity.FirstName,
                LastName = databaseEntity.LastName,
                NhsNumber = databaseEntity.NhsNumber?.ToString(),
                DateOfBirth = databaseEntity.DateOfBirth?.ToString("s"),
                AgeContext = databaseEntity.AgeContext,
                Nationality = databaseEntity.Nationality,
                Gender = databaseEntity.Gender,
                Restricted = databaseEntity.Restricted
            };
        }
        public static List<Domain.ResidentInformation> ToDomain(this IEnumerable<Person> people)
        {
            return people.Select(p => p.ToDomain()).ToList();
        }

        public static PhoneNumber ToDomain(this TelephoneNumber number)
        {
            return new PhoneNumber
            {
                Number = number.Number,
                Type = number.Type
            };
        }

        public static Domain.Address ToDomain(this Infrastructure.Address address)
        {
            return new Domain.Address
            {
                AddressLine1 = address.AddressLines,
                PostCode = address.PostCode,
                EndDate = address.EndDate,
                ContactAddressFlag = address.ContactAddressFlag,
                DisplayAddressFlag = address.DisplayAddressFlag
            };
        }

        public static CaseNoteInformation ToDomain(this CaseNote caseNote)
        {
            return new CaseNoteInformation
            {
                MosaicId = caseNote.PersonId.ToString(),
                CaseNoteId = caseNote.Id,
                CaseNoteTitle = caseNote.Title,
                CaseNoteContent = caseNote.Note,
                CreatedByEmail = caseNote.CreatedByWorker.EmailAddress,
                CreatedByName = $"{caseNote.CreatedByWorker.FirstNames} {caseNote.CreatedByWorker.LastNames}",
                CreatedOn = caseNote.CreatedOn,
                NoteType = caseNote.NoteType
            };
        }

        public static VisitInformation ToDomain(this Visit visit)
        {

            return new VisitInformation
            {
                VisitId = visit.VisitId,
                PersonId = visit.PersonId,
                VisitType = visit.VisitType,
                PlannedDateTime = visit.PlannedDateTime,
                ActualDateTime = visit.ActualDateTime,
                ReasonNotPlanned = visit.ReasonNotPlanned,
                ReasonVisitNotMade = visit.ReasonVisitNotMade,
                SeenAloneFlag = visit.SeenAloneFlag,
                CompletedFlag = visit.CompletedFlag,
                CpRegistrationId = visit.CpRegistrationId,
                CpVisitScheduleStepId = visit.CpVisitScheduleStepId,
                CpVisitScheduleDays = visit.CpVisitScheduleDays,
                CpVisitOnTime = visit.CpVisitOnTime,
                CreatedByEmail = visit.Worker.EmailAddress,
                CreatedByName = $"{visit.Worker.FirstNames} {visit.Worker.LastNames}"
            };
        }
    }
}
