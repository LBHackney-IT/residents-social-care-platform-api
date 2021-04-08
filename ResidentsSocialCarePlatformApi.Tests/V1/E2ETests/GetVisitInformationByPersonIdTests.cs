using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetVisitInformationByPersonIdTests : EndToEndTests<Startup>
    {
        [Test]
        public async Task WhenThereIsAVisitWithMatchingPersonId_Returns200AndVisitInformation()
        {
            var visitInformation = E2ETestHelpers.AddVisitToDatabase(SocialCareContext);
            var uri = new Uri($"api/v1/residents/{visitInformation.PersonId}/visits", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<List<VisitInformation>>(stringContent);

            convertedResponse.Should().BeEquivalentTo(new List<VisitInformation> { visitInformation.ToDomain().ToResponse() });
        }

        [Test]
        public async Task WhenThereIsNotAMatchingVisitForPersonId_Returns404()
        {
            const long nonExistentPersonId = 1234L;
            var uri = new Uri($"api/v1/residents/{nonExistentPersonId}/visits", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(404);
        }
    }
}
