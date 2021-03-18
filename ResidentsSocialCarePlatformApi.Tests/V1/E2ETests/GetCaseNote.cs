using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetCaseNote : EndToEndTests<Startup>
    {
        [Test]
        public async Task WhenThereIsAMatchingCaseNoteId_Returns200AndCaseNoteInformation()
        {
            var caseNote = E2ETestHelpers.AddCaseNoteWithNoteTypeAndWorkerToDatabase(SocialCareContext);
            var uri = new Uri($"api/v1/case-notes/{caseNote.CaseNoteId}", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<CaseNoteInformation>(stringContent);

            convertedResponse.Should().BeEquivalentTo(caseNote);
        }

        [Test]
        public async Task WhenThereIsNotAMatchingCaseNoteId_Returns404()
        {
            var nonExistentCaseNoteId = "1234";
            var uri = new Uri($"api/v1/case-notes/{nonExistentCaseNoteId}", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(404);
        }
    }
}
