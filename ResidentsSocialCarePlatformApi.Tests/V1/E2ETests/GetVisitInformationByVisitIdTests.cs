using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetVisitInformationByVisitIdTests : EndToEndTests<Startup>
    {
        [Test]
        public async Task WhenThereIsAVisitId_Returns200AndVisitInformation()
        {
            var visit = E2ETestHelpers.AddVisitToDatabase(SocialCareContext).ToDomain().ToResponse();
            var uri = new Uri($"api/v1/visits/{visit.VisitId}", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<VisitInformation>(stringContent);

            convertedResponse.Should().BeEquivalentTo(visit);
        }

        [Test]
        public async Task WhenThereIsNotAMatchingCaseNoteId_Returns404()
        {
            const long nonExistentVisitId = 12345L;
            var uri = new Uri($"api/v1/visits/{nonExistentVisitId}", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(404);
        }
    }
}
