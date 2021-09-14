using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Factories;
using VisitInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.VisitInformation;
using VisitInformationDomain = ResidentsSocialCarePlatformApi.V1.Domain.VisitInformation;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Factories.ResponseFactory
{
    public class ResponseFactoryVisitInformationTests
    {
        [Test]
        public void CanMapVisitInformationListFromDomainToResponse()
        {
            var plannedDateTime = new DateTime(2020, 4, 1, 20, 30, 00);
            var plannedDateTimeFormatted = plannedDateTime.ToString("s");

            var actualDateTime = new DateTime(1990, 11, 12, 10, 25, 00);
            var actualDateTimeFormatted = actualDateTime.ToString("s");

            var domain = new List<VisitInformationDomain>
            {
                new VisitInformationDomain
                {
                    VisitId = 000L,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = plannedDateTime,
                    ActualDateTime = actualDateTime,
                    ReasonNotPlanned = "ReasonNotPlanned",
                    ReasonVisitNotMade = "ReasonVisitNotMade",
                    SeenAloneFlag = "Y",
                    CompletedFlag = "Y",
                    CpRegistrationId = 000L,
                    CpVisitScheduleStepId = 000L,
                    CpVisitScheduleDays = 000L,
                    CpVisitOnTime = "Y"
                },
                new VisitInformationDomain
                {
                    VisitId = 000L,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = null,
                    ActualDateTime = null,
                    ReasonNotPlanned = null,
                    ReasonVisitNotMade = null,
                    SeenAloneFlag = null,
                    CompletedFlag = null,
                    CpRegistrationId = null,
                    CpVisitScheduleStepId = null,
                    CpVisitScheduleDays = null,
                    CpVisitOnTime = null
                },
                new VisitInformationDomain
                {
                    VisitId = 000L,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = plannedDateTime,
                    ActualDateTime = actualDateTime,
                    ReasonNotPlanned = "ReasonNotPlanned",
                    ReasonVisitNotMade = "ReasonVisitNotMade",
                    SeenAloneFlag = "N",
                    CompletedFlag = "N",
                    CpRegistrationId = 000L,
                    CpVisitScheduleStepId = 000L,
                    CpVisitScheduleDays = 000L,
                    CpVisitOnTime = "N"
                }
            };

            var expectedResponse = new List<VisitInformationResponse>
            {
                new VisitInformationResponse
                {
                    VisitId = 000L,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = plannedDateTimeFormatted,
                    ActualDateTime = actualDateTimeFormatted,
                    ReasonNotPlanned = "ReasonNotPlanned",
                    ReasonVisitNotMade = "ReasonVisitNotMade",
                    SeenAloneFlag = true,
                    CompletedFlag = true
                },
                new VisitInformationResponse
                {
                    VisitId = 000L,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = null,
                    ActualDateTime = null,
                    ReasonNotPlanned = null,
                    ReasonVisitNotMade = null,
                    SeenAloneFlag = false,
                    CompletedFlag = false
                },
                new VisitInformationResponse
                {
                    VisitId = 000L,
                    PersonId = 000L,
                    VisitType = "VisitType",
                    PlannedDateTime = plannedDateTimeFormatted,
                    ActualDateTime = actualDateTimeFormatted,
                    ReasonNotPlanned = "ReasonNotPlanned",
                    ReasonVisitNotMade = "ReasonVisitNotMade",
                    SeenAloneFlag = false,
                    CompletedFlag = false
                }
            };

            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }
    }
}
