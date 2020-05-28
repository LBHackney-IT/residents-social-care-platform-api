using AutoFixture;
using Bogus;
using MosaicResidentInformationApi.V1.Boundary.Responses;
using MosaicResidentInformationApi.V1.Infrastructure;
using Address = MosaicResidentInformationApi.V1.Infrastructure.Address;
using Person = MosaicResidentInformationApi.V1.Infrastructure.Person;
using ResidentInformation = MosaicResidentInformationApi.V1.Domain.ResidentInformation;
using System;

namespace MosaicResidentInformationApi.Tests.V1.Helper
{
    public static class TestHelper
    {
        public static ResidentInformation CreateDomainResident()
        {
            var faker = new Fixture();
            return faker.Create<ResidentInformation>();
        }

        public static Person CreateDatabasePersonEntity()
        {
            var faker = new Fixture();
            var fp = faker.Create<Person>();
            fp.DateOfBirth = new DateTime
                (fp.DateOfBirth.Year, fp.DateOfBirth.Month, fp.DateOfBirth.Day);
            return fp;
        }

        public static Address CreateDatabaseAddressForPersonId(int personId, string postcode = null)
        {
            var faker = new Fixture();

            var fa = faker.Build<Address>()
                .With(add => add.PersonId, personId)
                .Without(add => add.Person)
                .Create();

            fa.PostCode = postcode ?? fa.PostCode;
            return fa;
        }

        public static TelephoneNumber CreateDatabaseTelephoneNumberForPersonId(int personId)
        {
            var faker = new Fixture();

            return faker.Build<TelephoneNumber>()
                .With(tel => tel.PersonId, personId)
                .With(tel => tel.Type, PhoneType.Mobile.ToString)
                .Without(tel => tel.Person)
                .Create();
        }
    }
}
