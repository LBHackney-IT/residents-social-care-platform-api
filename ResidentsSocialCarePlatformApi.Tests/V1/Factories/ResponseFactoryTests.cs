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
                    EffectiveDate = recordOneTime,
                    CreatedOn = recordOneTime,
                    LastUpdatedOn = recordOneTime,
                    PersonVisitId = 456,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "Catra Grayskull",
                    CreatedByEmail = "catra@grayskull.com",
                    LastUpdatedName = "Catra Grayskull",
                    LastUpdatedEmail = "catra@grayskull.com",
                    RootCaseNoteId = 789,
                    CompletedDate = recordOneTime,
                    TimeoutDate = recordOneTime,
                    CopyOfCaseNoteId = 567,
                    CopiedDate = recordOneTime,
                    CopiedByName = "Catra Grayskull",
                    CopiedByEmail = "catra@grayskull.com"
                },
                new CaseNoteInformation
                {
                    MosaicId = "12345",
                    CaseNoteId = 100000,
                    CaseNoteTitle = "I AM ANOTHER CASE NOTE",
                    EffectiveDate = recordTwoTime,
                    CreatedOn = recordTwoTime,
                    LastUpdatedOn = recordTwoTime,
                    PersonVisitId = 123,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "A Name goes here",
                    CreatedByEmail = "some@email.com",
                    LastUpdatedName = "A Name goes here",
                    LastUpdatedEmail = "some@email.com",
                    RootCaseNoteId = 456,
                    CompletedDate = recordTwoTime,
                    TimeoutDate = recordTwoTime,
                    CopyOfCaseNoteId = 789,
                    CopiedDate = recordTwoTime,
                    CopiedByName = "This was copied",
                    CopiedByEmail = "copied@copy.com"
                }
            };

            var expectedResponse = new List<CaseNoteInformationResponse>
            {
                new CaseNoteInformationResponse
                {
                    MosaicId = "12345",
                    CaseNoteId = 67890,
                    CaseNoteTitle = "I AM A CASE NOTE",
                    EffectiveDate = formattedRecordOneTime,
                    CreatedOn = formattedRecordOneTime,
                    LastUpdatedOn = formattedRecordOneTime,
                    PersonVisitId = 456,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "Catra Grayskull",
                    CreatedByEmail = "catra@grayskull.com",
                    LastUpdatedName = "Catra Grayskull",
                    LastUpdatedEmail = "catra@grayskull.com",
                    RootCaseNoteId = 789,
                    CompletedDate = formattedRecordOneTime,
                    TimeoutDate = formattedRecordOneTime,
                    CopyOfCaseNoteId = 567,
                    CopiedDate = formattedRecordOneTime,
                    CopiedByName = "Catra Grayskull",
                    CopiedByEmail = "catra@grayskull.com"
                },
                new CaseNoteInformationResponse
                {
                    MosaicId = "12345",
                    CaseNoteId = 100000,
                    CaseNoteTitle = "I AM ANOTHER CASE NOTE",
                    EffectiveDate = formattedRecordTwoTime,
                    CreatedOn = formattedRecordTwoTime,
                    LastUpdatedOn = formattedRecordTwoTime,
                    PersonVisitId = 123,
                    NoteType = "Case Summary (ASC)",
                    CaseNoteContent = "",
                    CreatedByName = "A Name goes here",
                    CreatedByEmail = "some@email.com",
                    LastUpdatedName = "A Name goes here",
                    LastUpdatedEmail = "some@email.com",
                    RootCaseNoteId = 456,
                    CompletedDate = formattedRecordTwoTime,
                    TimeoutDate = formattedRecordTwoTime,
                    CopyOfCaseNoteId = 789,
                    CopiedDate = formattedRecordTwoTime,
                    CopiedByName = "This was copied",
                    CopiedByEmail = "copied@copy.com"
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
                EffectiveDate = dateTime,
                CreatedOn = dateTime,
                LastUpdatedOn = dateTime,
                PersonVisitId = 456,
                NoteType = "Case Summary (ASC)",
                CreatedByName = "Catra Grayskull",
                CreatedByEmail = "catra@grayskull.com",
                LastUpdatedName = "Catra Grayskull",
                LastUpdatedEmail = "catra@grayskull.com",
                CaseNoteContent = "I am case note content.",
                RootCaseNoteId = 789,
                CompletedDate = dateTime,
                TimeoutDate = dateTime,
                CopyOfCaseNoteId = 567,
                CopiedDate = dateTime,
                CopiedByName = "Catra Grayskull",
                CopiedByEmail = "catra@grayskull.com"
            };

            var expectedResponse = new CaseNoteInformationResponse
            {
                MosaicId = "12345",
                CaseNoteId = 67890,
                CaseNoteTitle = "I AM A CASE NOTE",
                EffectiveDate = formattedDateTime,
                CreatedOn = formattedDateTime,
                LastUpdatedOn = formattedDateTime,
                PersonVisitId = 456,
                NoteType = "Case Summary (ASC)",
                CreatedByName = "Catra Grayskull",
                CreatedByEmail = "catra@grayskull.com",
                LastUpdatedName = "Catra Grayskull",
                LastUpdatedEmail = "catra@grayskull.com",
                CaseNoteContent = "I am case note content.",
                RootCaseNoteId = 789,
                CompletedDate = formattedDateTime,
                TimeoutDate = formattedDateTime,
                CopyOfCaseNoteId = 567,
                CopiedDate = formattedDateTime,
                CopiedByName = "Catra Grayskull",
                CopiedByEmail = "catra@grayskull.com"
            };

            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void CanMapVisitInformationToSharedToResidentHistoricRecordVisit()
        {
            var person = TestHelper.CreateDatabasePersonEntity();
            var visit = TestHelper.CreateDatabaseVisit().Item1.ToDomain().ToResponse();

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
                Visit = visit
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
