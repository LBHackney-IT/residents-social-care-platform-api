using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class ListCaseNotesForAPerson : EndToEndTests<Startup>
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public async Task ReturnsAllCaseNotesForASpecificPerson()
        {

            var person = E2ETestHelpers.AddPersonToDatabase(SocialCareContext);

            var expectedCaseNoteResponseOne = E2ETestHelpers.AddCaseNoteForASpecificPersonToDb(SocialCareContext, person.Id);
            var expectedCaseNoteResponseTwo = E2ETestHelpers.AddCaseNoteForASpecificPersonToDb(SocialCareContext, person.Id);
            var expectedCaseNoteResponseThree = E2ETestHelpers.AddCaseNoteForASpecificPersonToDb(SocialCareContext, person.Id);

            var uri = new Uri($"api/v1/residents/{person.Id}/case-notes", UriKind.Relative);
            var response = Client.GetAsync(uri);

            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<CaseNoteInformationList>(stringContent);

            convertedResponse.CaseNotes.Should().ContainEquivalentOf(expectedCaseNoteResponseOne);
            convertedResponse.CaseNotes.Should().ContainEquivalentOf(expectedCaseNoteResponseTwo);
            convertedResponse.CaseNotes.Should().ContainEquivalentOf(expectedCaseNoteResponseThree);
        }
    }
}
