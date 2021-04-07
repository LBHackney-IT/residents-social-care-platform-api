using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using NUnit.Framework;
using Address = ResidentsSocialCarePlatformApi.V1.Infrastructure.Address;
using DomainAddress = ResidentsSocialCarePlatformApi.V1.Domain.Address;
using Person = ResidentsSocialCarePlatformApi.V1.Infrastructure.Person;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways
{
    [NonParallelizable]
    [TestFixture]
    public class SocialCareGatewayTests : DatabaseTests
    {
        private SocialCareGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new SocialCareGateway(SocialCareContext);
        }

        [Test]
        public void GetResidentInformationByPersonId_WhenThereAreNoMatchingRecords_ReturnsNull()
        {
            var response = _classUnderTest.GetEntityById(123, null);

            response.Should().BeNull();
        }

        [Test]
        public void GetResidentInformationByPersonId_ReturnsPersonalDetails()
        {
            var databaseEntity = AddPersonRecordToDatabase();
            var response = _classUnderTest.GetEntityById(databaseEntity.Id);

            response.FirstName.Should().Be(databaseEntity.FirstName);
            response.LastName.Should().Be(databaseEntity.LastName);
            response.NhsNumber.Should().Be(databaseEntity.NhsNumber?.ToString());
            response.DateOfBirth.Should().Be(databaseEntity.DateOfBirth?.ToString("s"));
            response.Should().NotBe(null);
        }

        [Test]
        public void GetResidentInformationByPersonId_ReturnsAddressDetails()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var response = _classUnderTest.GetEntityById(databaseEntity.Id);

            var expectedDomainAddress = new DomainAddress
            {
                AddressLine1 = address.AddressLines,
                PostCode = address.PostCode,
            };
            response.AddressList.Should().BeEquivalentTo(new List<DomainAddress> { expectedDomainAddress });
            response.Uprn.Should().Be(address.Uprn.ToString());
        }

        [Test]
        public void GetResidentInformationByPersonId_IfThereAreMultipleAddresses_ReturnsTheUprnForMostCurrentAddress()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var addressOld = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            addressOld.EndDate = DateTime.Now.AddDays(-67);
            SocialCareContext.Addresses.Add(addressOld);

            var addressCurrent = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            addressCurrent.EndDate = null;
            SocialCareContext.Addresses.Add(addressCurrent);
            SocialCareContext.SaveChanges();

            var response = _classUnderTest.GetEntityById(databaseEntity.Id);
            response.Uprn.Should().Be(addressCurrent.Uprn.ToString());
        }

        [Test]
        public void GetResidentInformationByPersonId_ReturnsContactDetails()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var phoneNumber = TestHelper.CreateDatabaseTelephoneNumberForPersonId(databaseEntity.Id);

            SocialCareContext.TelephoneNumbers.Add(phoneNumber);
            SocialCareContext.SaveChanges();

            var response = _classUnderTest.GetEntityById(databaseEntity.Id);
            response.PhoneNumberList.Should().BeEquivalentTo(new List<PhoneNumber>
            {
                new PhoneNumber {Number = phoneNumber.Number, Type = phoneNumber.Type}
            });
        }

        [Test]
        public void GetAllResidents_IfThereAreNoResidents_ReturnsAnEmptyList()
        {
            _classUnderTest.GetAllResidents(cursor: 0, limit: 20).Should().BeEmpty();
        }

        [Test]
        public void GetAllResident_IfThereAreResidentsWillReturnThem()
        {
            var databaseEntity = AddPersonRecordToDatabase();
            var databaseEntity1 = AddPersonRecordToDatabase();
            var databaseEntity2 = AddPersonRecordToDatabase();

            var listOfPersons = _classUnderTest.GetAllResidents(0, 20);

            listOfPersons.Should().ContainEquivalentOf(databaseEntity.ToDomain());
            listOfPersons.Should().ContainEquivalentOf(databaseEntity1.ToDomain());
            listOfPersons.Should().ContainEquivalentOf(databaseEntity2.ToDomain());
        }

        [Test]
        public void GetAllResidents_IfThereAreResidentAddressesWillReturnThem()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(0, 20);

            listOfPersons
                .Where(p => p.MosaicId.Equals(databaseEntity.Id.ToString()))
                .First()
                .AddressList
                .Should().ContainEquivalentOf(address.ToDomain());
        }

        [Test]
        public void GetAllResidents_IfThereAreResidentPhoneNumbersWillReturnThem()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var phoneNumber = TestHelper.CreateDatabaseTelephoneNumberForPersonId(databaseEntity.Id);
            var type = "Primary";
            phoneNumber.Type = type;
            SocialCareContext.TelephoneNumbers.Add(phoneNumber);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(0, 20);

            listOfPersons
                .Where(p => p.MosaicId.Equals(databaseEntity.Id.ToString()))
                .First()
                .PhoneNumberList
                .Should().ContainEquivalentOf(phoneNumber.ToDomain());
        }

        [Test]
        public void GetAllResidents_ReturnsResidentsUPRN()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(0, 20);

            listOfPersons
                .Where(p => p.MosaicId.Equals(databaseEntity.Id.ToString()))
                .First()
                .Uprn
                .Should().Be(address.Uprn.ToString());
        }

        [Test]
        public void GetAllResidentsWithFirstNameQueryParameter_ReturnsMatchingResident()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(firstname: "ciasom");
            var databaseEntity2 = AddPersonRecordToDatabase(firstname: "shape");
            var databaseEntity3 = AddPersonRecordToDatabase(firstname: "Ciasom");

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, firstname: "ciasom");

            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(databaseEntity1.ToDomain());
            listOfPersons.Should().ContainEquivalentOf(databaseEntity3.ToDomain());
        }

        [Test]
        public void GetAllResidentsWildcardSearchWithFirstNameQueryParameter_ReturnsMatchingResident()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(firstname: "ciasom");
            var databaseEntity2 = AddPersonRecordToDatabase(firstname: "shape");
            var databaseEntity3 = AddPersonRecordToDatabase(firstname: "Ciasom");

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, firstname: "iaso");

            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(databaseEntity1.ToDomain());
            listOfPersons.Should().ContainEquivalentOf(databaseEntity3.ToDomain());
        }

        [Test]
        public void GetAllResidentsWithLastNameQueryParameter_ReturnsMatchingResident()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(lastname: "tessellate");
            var databaseEntity2 = AddPersonRecordToDatabase(lastname: "square");
            var databaseEntity3 = AddPersonRecordToDatabase(lastname: "Tessellate");

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, lastname: "tessellate");

            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(databaseEntity1.ToDomain());
            listOfPersons.Should().ContainEquivalentOf(databaseEntity3.ToDomain());
        }

        [Test]
        public void GetAllResidentsWildcardSearchWithLastNameQueryParameter_ReturnsMatchingResident()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(lastname: "tessellate");
            var databaseEntity2 = AddPersonRecordToDatabase(lastname: "square");
            var databaseEntity3 = AddPersonRecordToDatabase(lastname: "Tessellate");

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, lastname: "sell");

            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(databaseEntity1.ToDomain());
            listOfPersons.Should().ContainEquivalentOf(databaseEntity3.ToDomain());
        }

        [Test]
        public void GetAllResidentsWithNameQueryParameters_ReturnsMatchingResidentOnlyOnce()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20,
                firstname: databaseEntity.FirstName, lastname: databaseEntity.LastName);

            listOfPersons.Count.Should().Be(1);
            listOfPersons.First().MosaicId.Should().Be(databaseEntity.Id.ToString());
        }

        [Test]
        public void GetAllResidentsWildcardSearchWithNameQueryParameters_ReturnsMatchingResidentOnlyOnce()
        {
            var databaseEntity = AddPersonRecordToDatabase(firstname: "ciasom", lastname: "Tessellate");

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id);
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, firstname: "ciasom", lastname: "ssellat");
            listOfPersons.Count.Should().Be(1);
            listOfPersons.First().MosaicId.Should().Be(databaseEntity.Id.ToString());
        }

        [Test]
        public void GetAllResidentsWithPostCodeQueryParameter_ReturnsMatchingResident()
        {
            var databaseEntity1 = AddPersonRecordToDatabase();
            var databaseEntity2 = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity1.Id, "E8 1DY");
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var address1 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity2.Id, "E8 5TG");
            SocialCareContext.Addresses.Add(address1);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, postcode: "E8 1DY");

            listOfPersons.Count.Should().Be(1);
            listOfPersons
                .First(p => p.MosaicId.Equals(databaseEntity1.Id.ToString()))
                .AddressList
                .Should().ContainEquivalentOf(address.ToDomain());
        }

        [Test]
        public void GetAllResidentsWithPostCodeQueryParameter_WhenAPersonHasTwoAddress_ReturnsOneRecordWithAListOfAddresses()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address1 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id, "E8 1DY");
            SocialCareContext.Addresses.Add(address1);
            SocialCareContext.SaveChanges();

            var address2 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id, "E8 1DY");
            SocialCareContext.Addresses.Add(address2);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, postcode: "E8 1DY").ToList();
            listOfPersons.Count.Should().Be(1);
            listOfPersons
                .First(p => p.MosaicId.Equals(databaseEntity.Id.ToString()))
                .AddressList.Count
                .Should().Be(2);
        }

        [Test]
        public void GetAllResidentsWithPostCodeQueryParameter_ReturnsPhoneNumberAndUprnWithResidentInformation()
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id, "E8 1DY");
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var phoneNumber = TestHelper.CreateDatabaseTelephoneNumberForPersonId(databaseEntity.Id);
            SocialCareContext.TelephoneNumbers.Add(phoneNumber);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, postcode: "E8 1DY").ToList();

            var personUnderTest = listOfPersons
                .First(p => p.MosaicId.Equals(databaseEntity.Id.ToString()));

            personUnderTest.PhoneNumberList.Should().ContainEquivalentOf(phoneNumber.ToDomain());
            personUnderTest.Uprn.Should().Be(address.Uprn.ToString());
        }

        [Test]
        public void GetAllResidentsWithNameAndPostCodeQueryParameter_ReturnsMatchingResident()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(firstname: "ciasom");
            var address1 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity1.Id, "E8 1DY");

            var databaseEntity2 = AddPersonRecordToDatabase(firstname: "wrong name");
            var address2 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity2.Id, "E8 1DY");

            var databaseEntity3 = AddPersonRecordToDatabase(firstname: "ciasom");
            var address3 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity3.Id, "E8 5RT");

            SocialCareContext.Addresses.AddRange(new List<Address> { address1, address2, address3 });
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, firstname: "ciasom", postcode: "E8 1DY").ToList();

            listOfPersons.Count.Should().Be(1);
            listOfPersons.First().MosaicId.Should().Be(databaseEntity1.Id.ToString());
            listOfPersons.First()
                .AddressList
                .Should().ContainEquivalentOf(address1.ToDomain());
        }

        [TestCase("E81DY")]
        [TestCase("e8 1DY")]
        public void GetAllResidentsWithPostCodeQueryParameter_IgnoresFormatting(string postcode)
        {
            var databaseEntity = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity.Id, postcode);
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, postcode: "E8 1DY");

            listOfPersons.Count.Should().Be(1);
            listOfPersons.First().MosaicId.Should().Be(databaseEntity.Id.ToString());
            listOfPersons.First().AddressList
                .Should().ContainEquivalentOf(address.ToDomain());
        }

        [TestCase("1 My Street")]
        [TestCase("My Street")]
        [TestCase("1 My Street, Hackney, London")]
        [TestCase("Hackney")]
        public void GetAllResidentsWithAddressQueryParameter_ReturnsMatchingResident(string addressQuery)
        {
            var databaseEntity1 = AddPersonRecordToDatabase();
            var databaseEntity2 = AddPersonRecordToDatabase();

            var address = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity1.Id, address: "1 My Street, Hackney, London");
            SocialCareContext.Addresses.Add(address);
            SocialCareContext.SaveChanges();

            var address1 = TestHelper.CreateDatabaseAddressForPersonId(databaseEntity2.Id, address: "5 Another Street, Lambeth, London");
            SocialCareContext.Addresses.Add(address1);
            SocialCareContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 20, address: addressQuery).ToList();
            listOfPersons.Count.Should().Be(1);
            listOfPersons
                .First(p => p.MosaicId.Equals(databaseEntity1.Id.ToString()))
                .AddressList
                .Should().ContainEquivalentOf(address.ToDomain());
        }


        [Test]
        public void GetAllResidentsOnlyReturnsTheLimit_ReturnsMatchingResidents()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(id: 1);
            var databaseEntity2 = AddPersonRecordToDatabase(id: 2);
            var databaseEntity3 = AddPersonRecordToDatabase(id: 3);
            var databaseEntity4 = AddPersonRecordToDatabase(id: 4);
            var databaseEntity5 = AddPersonRecordToDatabase(id: 5);

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 0, limit: 3).ToList();
            listOfPersons.Count.Should().Be(3);
            listOfPersons
                .Select(p => p.MosaicId)
                .Should().BeEquivalentTo(new List<string>
                {
                    databaseEntity1.Id.ToString(),
                    databaseEntity2.Id.ToString(),
                    databaseEntity3.Id.ToString()
                });
        }


        [Test]
        public void GetAllResidentsOnlyReturnsTheCursorAndTheLimit_ReturnsMatchingResidents()
        {
            var databaseEntity1 = AddPersonRecordToDatabase(id: 1);
            var databaseEntity2 = AddPersonRecordToDatabase(id: 2);
            var databaseEntity3 = AddPersonRecordToDatabase(id: 3);
            var databaseEntity4 = AddPersonRecordToDatabase(id: 4);
            var databaseEntity5 = AddPersonRecordToDatabase(id: 5);

            var listOfPersons = _classUnderTest.GetAllResidents(cursor: 2, limit: 3).ToList();
            listOfPersons.Count.Should().Be(3);
            listOfPersons
                .Select(p => p.MosaicId)
                .Should().BeEquivalentTo(new List<string>
                {
                    databaseEntity3.Id.ToString(),
                    databaseEntity4.Id.ToString(),
                    databaseEntity5.Id.ToString()
                });
        }

        [Test]
        public void InsertResidentReturnsResidentInformation()
        {
            var residentInformation = _classUnderTest.InsertResident(firstName: "Adora", lastName: "Grayskull");

            residentInformation.MosaicId.Should().NotBeNull();
            residentInformation.FirstName.Should().Be("Adora");
            residentInformation.LastName.Should().Be("Grayskull");
        }

        [Test]
        public void InsertResidentCreatesAPersonRecord()
        {
            var residentInformation = _classUnderTest.InsertResident(firstName: "Adora", lastName: "Grayskull");

            SocialCareContext.Persons.First().FirstName.Should().Be("Adora");
            SocialCareContext.Persons.First().LastName.Should().Be("Grayskull");
        }


        private Person AddPersonRecordToDatabase(string firstname = null, string lastname = null, int? id = null)
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(id, firstname, lastname);
            SocialCareContext.Persons.Add(databaseEntity);
            SocialCareContext.SaveChanges();
            return databaseEntity;
        }
    }
}
