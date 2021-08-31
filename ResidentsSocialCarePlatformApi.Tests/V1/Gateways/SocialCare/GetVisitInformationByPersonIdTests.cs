using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways.SocialCare
{
    [NonParallelizable]
    [TestFixture]
    public class GetVisitInformationByPersonIdTests : DatabaseTests
    {
        private SocialCareGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new SocialCareGateway(SocialCareContext);
        }

        [Test]
        public void WhenThereAreNoVisitsWithPersonId_ReturnsEmptyList()
        {
            const long realPersonId = 123L;
            const long fakePersonId = 456L;
            AddVisitToDatabase(personId: realPersonId);

            var response = _classUnderTest.GetVisitInformationByPersonId(fakePersonId);

            response.Should().BeEmpty();
        }

        [Test]
        public void WhenThereIsOneMatch_ReturnsAListContainingTheMatchingVisit()
        {
            var visit = AddVisitToDatabase();

            var response = _classUnderTest.GetVisitInformationByPersonId(visit.PersonId);

            response.ToList().Count.Should().Be(1);
            response.FirstOrDefault().VisitId.Should().Be(visit.VisitId);
        }

        [Test]
        public void WhenThereAreMultipleMatches_ReturnsAListContainingAllMatchingVisits()
        {
            const long realPersonId = 123L;
            const long visitIdOne = 1L;
            const long visitIdTwo = 2L;

            AddVisitToDatabase(visitIdOne, personId: realPersonId);
            AddVisitToDatabase(visitIdTwo, personId: realPersonId);

            var response = _classUnderTest.GetVisitInformationByPersonId(realPersonId);

            response.ToList().Count.Should().Be(2);
        }

        [Test]
        public void OnlyReturnVisitsForCorrectPersonId()
        {
            const long realPersonId = 123L;
            const long otherPersonId = 456L;
            const long visitIdOne = 1L;
            const long visitIdTwo = 2L;

            AddVisitToDatabase(visitIdOne, personId: realPersonId);
            AddVisitToDatabase(visitIdTwo, personId: otherPersonId);

            var response = _classUnderTest.GetVisitInformationByPersonId(realPersonId);

            response.ToList().Count.Should().Be(1);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsDetailsFromVisit()
        {
            var visit = AddVisitToDatabase();

            var response = _classUnderTest.GetVisitInformationByPersonId(visit.PersonId).First();

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.VisitId.Should().Be(visit.VisitId);
            response.VisitType.Should().Be(visit.VisitType);
            response.PlannedDateTime.Should().Be(visit.PlannedDateTime);
            response.ActualDateTime.Should().Be(visit.ActualDateTime);
            response.ReasonVisitNotMade.Should().Be(visit.ReasonVisitNotMade);
            response.SeenAloneFlag.Should().Be(visit.SeenAloneFlag);
            response.CompletedFlag.Should().Be(visit.CompletedFlag);
            response.CpRegistrationId.Should().Be(visit.CpRegistrationId);
            response.CpVisitScheduleStepId.Should().Be(visit.CpVisitScheduleStepId);
            response.CpVisitScheduleDays.Should().Be(visit.CpVisitScheduleDays);
            response.CpVisitOnTime.Should().Be(visit.CpVisitOnTime);
            response.CreatedByEmail.Should().Be(visit.Worker.EmailAddress);
            response.CreatedByName.Should().Be($"{visit.Worker.FirstNames} {visit.Worker.LastNames}");
        }

        private Visit AddVisitToDatabase(long? visitId = null, long? workerId = null, long? personId = null)
        {
            var visit= TestHelper.CreateDatabaseVisit(visitId, personId, workerId: workerId);

            SocialCareContext.Visits.Add(visit);
            SocialCareContext.Workers.Add(visit.Worker);
            SocialCareContext.SaveChanges();

            return visit;
        }
    }
}
