using AutoFixture;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase;

namespace ResidentsSocialCarePlatformApi.Tests.V1.UseCase
{
    public class GetRelationshipsByPersonIdTests
    {
        private Mock<ISocialCareGateway> _mockSocialCareGateway;
        private GetRelationshipsByPersonIdUseCase _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockSocialCareGateway = new Mock<ISocialCareGateway>();
            _classUnderTest = new GetRelationshipsByPersonIdUseCase(_mockSocialCareGateway.Object);
        }

        [Test]
        public void WhenThereIsNoPersonalRelationships_ReturnsEmptyList()
        {
            PersonalRelationships noMatchingRelationship = new PersonalRelationships();
            _mockSocialCareGateway.Setup(x => x.GetPersonalRelationships(123)).Returns(noMatchingRelationship);

            var response = _classUnderTest.Execute(123);

            response.PersonId.Equals(123);

            response.PersonalRelationships.Parents.Should().BeEmpty();
            response.PersonalRelationships.Siblings.Should().BeEmpty();
            response.PersonalRelationships.Children.Should().BeEmpty();
            response.PersonalRelationships.Other.Should().BeEmpty();
        }

        [Test]
        public void WhenThereArePersonalRelationships_ReturnsListOfIDs()
        {
            PersonalRelationships matchingRelationhips = _fixture.Create<PersonalRelationships>();
            _mockSocialCareGateway.Setup(x => x.GetPersonalRelationships(123)).Returns(matchingRelationhips);

            var response = _classUnderTest.Execute(123);

            response.PersonId.Equals(123);

            response.PersonalRelationships.Parents.Should().NotBeEmpty();
            response.PersonalRelationships.Siblings.Should().NotBeEmpty();
            response.PersonalRelationships.Children.Should().NotBeEmpty();
            response.PersonalRelationships.Other.Should().NotBeEmpty();
        }
    }
}
