using System.Collections.Generic;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using NUnit.Framework;
using Address = ResidentsSocialCarePlatformApi.V1.Domain.Address;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Domain.ResidentInformation;
using ResidentInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Factories
{
    public class ResponseFactoryTests
    {
        [Test]
        public void CanMapResidentInformationFromDomainToResponse()
        {
            var domain = new ResidentInformation
            {
                Uprn = "uprn",
                AddressList = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "addess11",
                        AddressLine2 = "address22",
                        AddressLine3 = "address33",
                        PostCode = "Postcode"
                    }
                },
                FirstName = "Name",
                LastName = "Last",
                NhsNumber = "nhs",
                DateOfBirth = "DOB",
                PhoneNumberList = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Number = "number",
                        Type = "Fax"
                    }
                },
                Restricted = "N"
            };

            var expectedResponse = new ResidentInformationResponse
            {
                Uprn = "uprn",
                AddressList = new List<ResidentsSocialCarePlatformApi.V1.Boundary.Responses.Address>
                {
                    new ResidentsSocialCarePlatformApi.V1.Boundary.Responses.Address()
                    {
                        AddressLine1 = "addess11",
                        AddressLine2 = "address22",
                        AddressLine3 = "address33",
                        PostCode = "Postcode"
                    }
                },
                FirstName = "Name",
                LastName = "Last",
                NhsNumber = "nhs",
                DateOfBirth = "DOB",
                PhoneNumber = new List<Phone>
                {
                    new Phone
                    {
                        PhoneNumber = "number",
                        PhoneType = "Fax"
                    }
                },
                Restricted = "N"
            };
            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void CanMapResidentInformationWithOnlyPersonalInformationFromDomainToResponse()
        {
            var domain = new ResidentInformation
            {
                Uprn = "uprn",
                AddressList = null,
                FirstName = "Name",
                LastName = "Last",
                NhsNumber = "nhs",
                DateOfBirth = "DOB",
                PhoneNumberList = null,
                Restricted = null
            };

            var expectedResponse = new ResidentInformationResponse
            {
                Uprn = "uprn",
                AddressList = null,
                FirstName = "Name",
                LastName = "Last",
                NhsNumber = "nhs",
                DateOfBirth = "DOB",
                PhoneNumber = null,
                Restricted = null
            };
            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }
    }
}
