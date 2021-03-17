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

        public static List<Boundary.Responses.CaseNoteInformation> ToResponse(this IEnumerable<Domain.CaseNoteInformation> caseNotes)
        {
            return caseNotes.Select(caseNote => new Boundary.Responses.CaseNoteInformation
            {
                MosaicId = caseNote.MosaicId,
                CaseNoteId = caseNote.CaseNoteId,
                CaseNoteTitle = caseNote.CaseNoteTitle,
                EffectiveDate = caseNote.EffectiveDate.ToString("s"),
                CreatedOn = caseNote.CreatedOn.ToString("s"),
                LastUpdatedOn = caseNote.LastUpdatedOn.ToString("s")
            }).ToList();
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
    }
}
