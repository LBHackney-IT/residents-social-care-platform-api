using System.Linq;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using NUnit.Framework;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Infrastructure
{
    [TestFixture]
    public class SocialCareContextTests : DatabaseTests
    {
        [Test]
        public void CanCreateADatabaseRecordForAPerson()
        {
            var person = TestHelper.CreateDatabasePersonEntity();

            SocialCareContext.Add(person);
            SocialCareContext.SaveChanges();

            var result = SocialCareContext.Persons.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(person);
        }

        [Test]
        public void CanCreateADatabaseRecordForACaseNote()
        {
            var person = TestHelper.CreateDatabasePersonEntity();
            var caseNote = TestHelper.CreateDatabaseCaseNote(person.Id);

            SocialCareContext.Add(caseNote);
            SocialCareContext.SaveChanges();

            var result = SocialCareContext.CaseNotes.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(caseNote);
        }

        [Test]
        public void CanCreateADatabaseRecordForAVisit()
        {
            var visit = TestHelper.CreateDatabaseVisit();

            SocialCareContext.Add(visit);
            SocialCareContext.SaveChanges();

            var result = SocialCareContext.Visits.FirstOrDefault();

            result.Should().BeEquivalentTo(visit);
        }

        [Test]
        public void CanCreateADatabaseRecordForAnOrganisation()
        {
            var organisation = TestHelper.CreateDatabaseOrganisation();

            SocialCareContext.Add(organisation);
            SocialCareContext.SaveChanges();

            var result = SocialCareContext.Organisations.FirstOrDefault();

            result.Should().BeEquivalentTo(organisation);
        }
    }
}
