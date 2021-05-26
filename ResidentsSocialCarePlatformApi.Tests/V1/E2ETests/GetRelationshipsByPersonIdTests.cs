using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;

namespace ResidentsSocialCarePlatformApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetRelationshipsByPersonIdTests : EndToEndTests<Startup>
    {
        [Test]
        public async Task WhenThereArePersonalRelationships_Returns200AndRelationships()
        {
            var (person, otherPerson1, otherPerson2) = E2ETestHelpers.AddPersonalRelationshipsToDatabase(SocialCareContext, familyCategory: "Child's Parents");
            var uri = new Uri($"api/v1/residents/{person.Id}/relationships", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<Relationships>(stringContent);

            convertedResponse.PersonId.Should().Be(person.Id);
            convertedResponse.PersonalRelationships.Parents.Should().Contain(otherPerson1.Id);
            convertedResponse.PersonalRelationships.Parents.Should().Contain(otherPerson2.Id);
            convertedResponse.PersonalRelationships.Children.Should().BeEmpty();
            convertedResponse.PersonalRelationships.Siblings.Should().BeEmpty();
            convertedResponse.PersonalRelationships.Other.Should().BeEmpty();
        }

        [Test]
        public async Task WhenThereAreNoPersonalRelationships_Returns200AndEmptyLists()
        {
            var person = E2ETestHelpers.AddPersonToDatabase(SocialCareContext);
            var uri = new Uri($"api/v1/residents/{person.Id}/relationships", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<Relationships>(stringContent);

            convertedResponse.PersonId.Should().Be(person.Id);
            convertedResponse.PersonalRelationships.Parents.Should().BeEmpty();
            convertedResponse.PersonalRelationships.Children.Should().BeEmpty();
            convertedResponse.PersonalRelationships.Siblings.Should().BeEmpty();
            convertedResponse.PersonalRelationships.Other.Should().BeEmpty();
        }

        [Test]
        public async Task WhenThereIsAnInvalidPersonId_Returns400()
        {
            var invalidPersonId = 0;
            var uri = new Uri($"api/v1/residents/{invalidPersonId}/relationships", UriKind.Relative);

            var response = Client.GetAsync(uri);

            response.Result.StatusCode.Should().Be(400);
        }
    }
}
