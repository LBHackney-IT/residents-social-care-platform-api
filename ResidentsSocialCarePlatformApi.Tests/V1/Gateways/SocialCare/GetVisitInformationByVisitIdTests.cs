using System;
using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

#nullable enable
namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways.SocialCare
{
    [NonParallelizable]
    [TestFixture]
    public class GetVisitInformationByVisitIdTests : DatabaseTests

    {
        private SocialCareGateway _classUnderTest = null!;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new SocialCareGateway(SocialCareContext);
        }

        [Test]
        public void WhenThereIsNoMatchingVisit_ReturnsNull()
        {
            const long realVisitId = 123L;
            const long fakeVisitId = 456L;
            AddVisitToDatabase(realVisitId);

            var response = _classUnderTest.GetVisitInformationByVisitId(fakeVisitId);

            response.Should().BeNull();
        }

        [Test]
        public void WhenThereAreMultipleVisits_ReturnsVisitsWithMatchingId()
        {
            var visit = AddVisitToDatabase();
            AddVisitToDatabase();

            var response = _classUnderTest.GetVisitInformationByVisitId(visit.VisitId);

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.VisitId.Should().Be(visit.VisitId);
        }

        [Test]
        public void WhenThereIsAMatchingVisit_ReturnsVisitDetails()
        {
            var visit = AddVisitToDatabase();

            var response = _classUnderTest.GetVisitInformationByVisitId(visit.VisitId);

            if (response == null)
            {
                throw new ArgumentNullException();
            }

            response.VisitId.Should().Be(visit.VisitId);
            response.PersonId.Should().Be(visit.PersonId);
            response.VisitType.Should().Be(visit.VisitType);
            response.PlannedDateTime.Should().Be(visit.PlannedDateTime);
            response.ActualDateTime.Should().Be(visit.ActualDateTime);
            response.ReasonNotPlanned.Should().Be(visit.ReasonNotPlanned);
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

        private Visit AddVisitToDatabase(long? visitId = null, long? workerId = null)
        {
            var visit = TestHelper.CreateDatabaseVisit(visitId, workerId: workerId);

            SocialCareContext.Visits.Add(visit);
            SocialCareContext.Workers.Add(visit.Worker);
            SocialCareContext.SaveChanges();

            return visit;
        }
    }
}
