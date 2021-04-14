using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;


namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetResidentRecords : EndToEndTests<Startup>
    {
        [Test]
        public async Task WhenThereIsAVisitWithMatchingPersonId_Returns200AndResidentRecords()
        {
            var visit = E2ETestHelpers.AddVisitToDatabase(SocialCareContext);
            var visitResponse = visit.ToDomain().ToResponse().ToSharedResponse(visit.PersonId);
            var uri = new Uri($"api/v1/residents/{visit.PersonId}/records", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<ResidentHistoricRecord>>(stringContent);

            convertedResponse.Should().BeEquivalentTo(new List<ResidentHistoricRecord> { visitResponse });
        }

        [Test]
        public async Task WhenThereIsACaseNoteWithMatchingPersonId_Returns200AndResidentRecords()
        {
            var person = E2ETestHelpers.AddPersonToDatabase(SocialCareContext);
            var caseNote = E2ETestHelpers.AddCaseNoteForASpecificPersonToDb(SocialCareContext, person.Id);
            var caseNotesResponse = caseNote.ToSharedResponse(person.Id);
            var uri = new Uri($"api/v1/residents/{person.Id}/records", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<ResidentHistoricRecord>>(stringContent);

            convertedResponse.Should().BeEquivalentTo(new List<ResidentHistoricRecord> { caseNotesResponse });
        }
    }
}
