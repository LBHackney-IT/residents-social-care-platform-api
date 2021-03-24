using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetVisitInformationByPersonIdTests : EndToEndTests<Startup>
    {
        [Test]
        public async Task WhenThereIsAVisitWithMatchingPersonId_Returns200AndVisitInformation()
        {
            var visitInformation = E2ETestHelpers.AddVisitToDatabase(SocialCareContext);
            var uri = new Uri($"api/v1/visit-information/person-id/{visitInformation.PersonId}", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<VisitInformation>(stringContent);

            convertedResponse.Should().BeEquivalentTo(visitInformation);
        }

        [Test]
        public async Task WhenThereIsNotAMatchingVisitForPersonId_Returns404()
        {
            var nonExistentPersonId = "1234";
            var uri = new Uri($"api/v1/visit-information/person-id/{nonExistentPersonId}", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(404);
        }
    }
}
