using System;
using System.Collections.Generic;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Factories;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using Address = ResidentsSocialCarePlatformApi.V1.Domain.Address;
using CaseNoteInformation = ResidentsSocialCarePlatformApi.V1.Domain.CaseNoteInformation;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Domain.ResidentInformation;
using ResidentInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;
using CaseNoteInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.CaseNoteInformation;

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

        [Test]
        public void CanMapCaseNoteInformationListFromDomainToResponse()
        {
            var recordOneTime = new DateTime(2020, 4, 1, 20, 30, 00);
            const string formattedRecordOneTime = "2020-04-01T20:30:00";

            var recordTwoTime = new DateTime(1990, 11, 12, 10, 25, 00);
            const string formattedRecordTwoTime = "1990-11-12T10:25:00";

            var domain = new List<CaseNoteInformation>
            {
                new CaseNoteInformation
                {
                    MosaicId = "12345",
                    CaseNoteId = 67890,
                    CaseNoteTitle = "I AM A CASE NOTE",
                    CreatedOn = recordOneTime,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "Catra Grayskull",
                    CreatedByEmail = "catra@grayskull.com"
                },
                new CaseNoteInformation
                {
                    MosaicId = "12345",
                    CaseNoteId = 100000,
                    CaseNoteTitle = "I AM ANOTHER CASE NOTE",
                    CreatedOn = recordTwoTime,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "A Name goes here",
                    CreatedByEmail = "some@email.com"
                }
            };

            var expectedResponse = new List<CaseNoteInformationResponse>
            {
                new CaseNoteInformationResponse
                {
                    MosaicId = "12345",
                    CaseNoteId = 67890,
                    CaseNoteTitle = "I AM A CASE NOTE",
                    CreatedOn = formattedRecordOneTime,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "Catra Grayskull",
                    CreatedByEmail = "catra@grayskull.com",
                },
                new CaseNoteInformationResponse
                {
                    MosaicId = "12345",
                    CaseNoteId = 100000,
                    CaseNoteTitle = "I AM ANOTHER CASE NOTE",
                    CreatedOn = formattedRecordTwoTime,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "A Name goes here",
                    CreatedByEmail = "some@email.com",
                }
            };

            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void CanMapCaseNoteInformationFromDomainToResponse()
        {
            var dateTime = new DateTime(2021, 3, 1, 15, 30, 00);
            const string formattedDateTime = "2021-03-01T15:30:00";

            var domain = new CaseNoteInformation
            {
                MosaicId = "12345",
                CaseNoteId = 67890,
                CaseNoteTitle = "I AM A CASE NOTE",
                CreatedOn = dateTime,
                NoteType = "Case Summary (ASC)",
                CreatedByName = "Catra Grayskull",
                CreatedByEmail = "catra@grayskull.com",
                CaseNoteContent = "I am case note content."
            };

            var expectedResponse = new CaseNoteInformationResponse
            {
                MosaicId = "12345",
                CaseNoteId = 67890,
                CaseNoteTitle = "I AM A CASE NOTE",
                CreatedOn = formattedDateTime,
                NoteType = "Case Summary (ASC)",
                CreatedByName = "Catra Grayskull",
                CreatedByEmail = "catra@grayskull.com",
                CaseNoteContent = "I am case note content.",
            };

            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void CanMapVisitInformationToSharedToResidentHistoricRecordVisit()
        {
            var person = TestHelper.CreateDatabasePersonEntity();
            var visit = TestHelper.CreateDatabaseVisit().ToDomain().ToResponse();

            var residentHistoricRecordVisit = new ResidentHistoricRecordVisit
            {
                RecordId = visit.VisitId,
                FormName = "",
                PersonId = person.Id,
                FirstName = "",
                LastName = "",
                DateOfBirth = "",
                OfficerEmail = visit.CreatedByEmail,
                CaseFormUrl = "",
                CaseFormTimeStamp = "",
                DateOfEvent = visit.ActualDateTime ?? visit.PlannedDateTime,
                CaseNoteTitle = "",
                RecordType = RecordType.Visit,
                Visit = visit,
                IsHistoric = true
            };

            visit.ToSharedResponse(person.Id).Should().BeEquivalentTo(residentHistoricRecordVisit);
        }

        [Test]
        public void CanMapCaseNoteInformationToSharedToResidentHistoricRecordCaseNote()
        {
            var person = TestHelper.CreateDatabasePersonEntity();
            var caseNote = TestHelper.CreateDatabaseCaseNote().ToDomain().ToResponse();

            var residentHistoricRecordCaseNote = new ResidentHistoricRecordCaseNote
            {
                RecordId = caseNote.CaseNoteId,
                FormName = "",
                PersonId = person.Id,
                FirstName = "",
                LastName = "",
                DateOfBirth = "",
                OfficerEmail = caseNote.CopiedByEmail,
                CaseFormUrl = "",
                CaseFormTimeStamp = "",
                DateOfEvent = caseNote.CompletedDate,
                CaseNoteTitle = caseNote.CaseNoteTitle,
                RecordType = RecordType.Visit,
                CaseNote = caseNote
            };

            caseNote.ToSharedResponse(person.Id).Should().BeEquivalentTo(caseNote.ToSharedResponse(person.Id));
        }
    }
}
