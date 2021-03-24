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
            AddVisitWithAPerson(personId: realPersonId);

            var response = _classUnderTest.GetVisitInformationByPersonId(fakePersonId);

            response.Should().BeEmpty();
        }

        [Test]
        public void WhenThereIsOneMatch_ReturnsAListContainingTheMatchingVisit()
        {
            const long realPersonId = 123L;
            var visit = AddVisitWithAPerson();

            var response = _classUnderTest.GetVisitInformationByPersonId(realPersonId);

            response.Count.Should().Be(1);
            response.FirstOrDefault().VisitId.Should().Be(visit.VisitId);
        }

        [Test]
        public void WhenThereAreMultipleMatches_ReturnsAListContainingAllMatchingCaseNotes()
        {
            const long realPersonId = 123L;
            AddVisitWithAPerson(realPersonId);
            AddVisitWithAPerson(realPersonId);

            var response = _classUnderTest.GetVisitInformationByPersonId(realPersonId);

            response.Count.Should().Be(2);
        }

        [Test]
        public void OnlyReturnVisitsForCorrectPersonId()
        {
            const long realPersonId = 123L;
            const long otherPersonId = 456L;
            AddVisitWithAPerson(personId: realPersonId);
            AddVisitWithAPerson(personId: otherPersonId);

            var response = _classUnderTest.GetVisitInformationByPersonId(realPersonId);

            response.Count.Should().Be(1);
        }

        [Test]
        public void WhenThereIsAMatchingRecord_ReturnsDetailsFromVisit()
        {
            var visit = AddVisitWithAPerson();

            var response = _classUnderTest.GetVisitInformationByPersonId(visit.PersonId).First();

            response.VisitId.Should().Be(visit.VisitId);
            response.PersonId.Should().Be(visit.PersonId);
            response.VisitType.Should().Be(visit.VisitType);
            response.PlannedDateTime.Should().Be(visit.PlannedDateTime);
            response.ActualDateTime.Should().Be(visit.ActualDateTime);
            response.ReasonVisitNotMade.Should().Be(visit.ReasonVisitNotMade);
            response.SeenAloneFlag.Should().Be(visit.SeenAloneFlag);
            response.CompletedFlag.Should().Be(visit.CompletedFlag);
            response.OrgId.Should().Be(visit.OrgId);
            response.WorkerId.Should().Be(visit.WorkerId);
            response.CpRegistrationId.Should().Be(visit.CpRegistrationId);
            response.CpVisitScheduleStepId.Should().Be(visit.CpVisitScheduleStepId);
            response.CpVisitScheduleDays.Should().Be(visit.CpVisitScheduleDays);
            response.CpVisitOnTime.Should().Be(visit.CpVisitOnTime);
        }

        private Visit AddVisitWithAPerson(long id = 123, long personId = 123)
        {
            var visit = TestHelper.CreateDatabaseVisit(visitId: id, personId: personId);

            SocialCareContext.Visits.Add(visit);
            SocialCareContext.SaveChanges();

            return visit;
        }
    }
}
