using System;
using System.Collections.Generic;
using System.Linq;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using Address = ResidentsSocialCarePlatformApi.V1.Domain.Address;
using DbAddress = ResidentsSocialCarePlatformApi.V1.Infrastructure.Address;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Domain.ResidentInformation;

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
                DateOfBirth = databaseEntity.DateOfBirth?.ToString("O"),
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
    }
}
