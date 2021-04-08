using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways.SocialCare
{
    [NonParallelizable]
    [TestFixture]
    public class GetVisitInformationByVisitIdTests : DatabaseTests

    {
        private SocialCareGateway _classUnderTest;

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
            const long visitIdOne = 123L;
            const long visitIdTwo = 456L;

            var visitOne = AddVisitToDatabase(visitIdOne);
            var visitTwo = AddVisitToDatabase(visitIdTwo);

            var response = _classUnderTest.GetVisitInformationByVisitId(visitOne.VisitId);

            response.VisitId.Should().Be(visitIdOne);
        }

        [Test]
        public void WhenThereIsAMatchingVisit_ReturnsVisitDetails()
        {
            const long realVisitId = 123L;
            var visitInformation = AddVisitToDatabase(realVisitId);

            var response = _classUnderTest.GetVisitInformationByVisitId(visitInformation.VisitId);

            response.VisitId.Should().Be(visitInformation.VisitId);
            response.VisitType.Should().Be(visitInformation.VisitType);
            response.PlannedDateTime.Should().Be(visitInformation.PlannedDateTime);
            response.ActualDateTime.Should().Be(visitInformation.ActualDateTime);
            response.ReasonNotPlanned.Should().Be(visitInformation.ReasonNotPlanned);
            response.ReasonVisitNotMade.Should().Be(visitInformation.ReasonVisitNotMade);
            response.SeenAloneFlag.Should().Be(visitInformation.SeenAloneFlag);
            response.CompletedFlag.Should().Be(visitInformation.CompletedFlag);
            response.CpRegistrationId.Should().Be(visitInformation.CpRegistrationId);
            response.CpVisitScheduleStepId.Should().Be(visitInformation.CpVisitScheduleStepId);
            response.CpVisitScheduleDays.Should().Be(visitInformation.CpVisitScheduleDays);
            response.CpVisitOnTime.Should().Be(visitInformation.CpVisitOnTime);
        }

        private Visit AddVisitToDatabase(long id)
        {
            var visit = TestHelper.CreateDatabaseVisit(visitId: id);

            SocialCareContext.Visits.Add(visit);
            SocialCareContext.SaveChanges();

            return visit;
        }
    }
}
