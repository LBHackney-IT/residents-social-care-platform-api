using System.Collections.Generic;
using System.Linq;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Domain;

namespace ResidentsSocialCarePlatformApi.V1.Factories
{
    public static class ResponseFactory
    {
        public static Boundary.Responses.ResidentInformation ToResponse(this Domain.ResidentInformation domain)
        {
            return new Boundary.Responses.ResidentInformation
            {
                MosaicId = domain.MosaicId,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                NhsNumber = domain.NhsNumber,
                Nationality = domain.Nationality,
                Gender = domain.Gender,
                DateOfBirth = domain.DateOfBirth,
                AgeContext = domain.AgeContext,
                Uprn = domain.Uprn,
                AddressList = domain.AddressList?.ToResponse(),
                PhoneNumber = domain.PhoneNumberList?.ToResponse(),
                Restricted = domain.Restricted
            };
        }

        public static List<Boundary.Responses.ResidentInformation> ToResponse(this IEnumerable<Domain.ResidentInformation> people)
        {
            return people.Select(p => p.ToResponse()).ToList();
        }

        public static Boundary.Responses.CaseNoteInformation ToResponse(this Domain.CaseNoteInformation caseNote)
        {
            return new Boundary.Responses.CaseNoteInformation
            {
                MosaicId = caseNote.MosaicId,
                CaseNoteId = caseNote.CaseNoteId,
                CaseNoteTitle = caseNote.CaseNoteTitle,
                EffectiveDate = caseNote.EffectiveDate?.ToString("s"),
                CreatedOn = caseNote.CreatedOn?.ToString("s"),
                LastUpdatedOn = caseNote.LastUpdatedOn?.ToString("s"),
                PersonVisitId = caseNote.PersonVisitId,
                NoteType = caseNote.NoteType,
                CreatedByName = caseNote.CreatedByName,
                CreatedByEmail = caseNote.CreatedByEmail,
                LastUpdatedName = caseNote.LastUpdatedName,
                LastUpdatedEmail = caseNote.LastUpdatedEmail,
                CaseNoteContent = caseNote.CaseNoteContent,
                RootCaseNoteId = caseNote.RootCaseNoteId,
                CompletedDate = caseNote.CompletedDate?.ToString("s"),
                TimeoutDate = caseNote.TimeoutDate?.ToString("s"),
                CopyOfCaseNoteId = caseNote.CopyOfCaseNoteId,
                CopiedDate = caseNote.CopiedDate?.ToString("s"),
                CopiedByName = caseNote.CopiedByName,
                CopiedByEmail = caseNote.CopiedByEmail
            };
        }

        public static List<Boundary.Responses.CaseNoteInformation> ToResponse(this IEnumerable<Domain.CaseNoteInformation> caseNotes)
        {
            return caseNotes.Select(caseNote => caseNote.ToResponse()).ToList();
        }

        private static List<Phone> ToResponse(this List<PhoneNumber> phoneNumbers)
        {
            return phoneNumbers.Select(number => new Phone
            {
                PhoneNumber = number.Number,
                PhoneType = number.Type
            }).ToList();
        }

        private static List<Boundary.Responses.Address> ToResponse(this List<Domain.Address> addresses)
        {
            return addresses.Select(add => new Boundary.Responses.Address
            {
                EndDate = add.EndDate,
                ContactAddressFlag = add.ContactAddressFlag,
                DisplayAddressFlag = add.DisplayAddressFlag,
                AddressLine1 = add.AddressLine1,
                AddressLine2 = add.AddressLine2,
                AddressLine3 = add.AddressLine3,
                PostCode = add.PostCode
            }).ToList();
        }

        public static List<Boundary.Responses.VisitInformation> ToResponse(this IEnumerable<Domain.VisitInformation> visits)
        {
            return visits.Select(visit => visit.ToResponse()).ToList();
        }

        public static Boundary.Responses.VisitInformation ToResponse(this Domain.VisitInformation visit)
        {
            return new Boundary.Responses.VisitInformation
            {
                VisitId = visit.VisitId,
                PersonId = visit.PersonId,
                VisitType = visit.VisitType,
                PlannedDateTime = visit.PlannedDateTime?.ToString("s"),
                ActualDateTime = visit.ActualDateTime?.ToString("s"),
                ReasonNotPlanned = visit.ReasonNotPlanned,
                ReasonVisitNotMade = visit.ReasonVisitNotMade,
                SeenAloneFlag = !string.IsNullOrEmpty(visit.SeenAloneFlag) && visit.SeenAloneFlag.Equals("Y"),
                CompletedFlag = !string.IsNullOrEmpty(visit.CompletedFlag) && visit.CompletedFlag.Equals("Y"),
                CpRegistrationId = visit.CpRegistrationId,
                CpVisitScheduleStepId = visit.CpVisitScheduleStepId,
                CpVisitScheduleDays = visit.CpVisitScheduleDays,
                CpVisitOnTime = !string.IsNullOrEmpty(visit.CpVisitOnTime) && visit.CpVisitOnTime == "Y",
                CreatedByName = visit.CreatedByName,
                CreatedByEmail = visit.CreatedByEmail
            };
        }
    }
}
