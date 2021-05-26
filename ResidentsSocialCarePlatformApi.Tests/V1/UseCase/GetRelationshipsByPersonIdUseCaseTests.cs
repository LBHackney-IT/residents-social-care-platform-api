using AutoFixture;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;

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
        public void WhenThereAreNoPersonalRelationships_ReturnsEmptyLists()
        {
            var request = new GetRelationshipsRequest() { PersonId = 123456789 };
            PersonalRelationships noMatchingRelationships = new PersonalRelationships();
            _mockSocialCareGateway.Setup(x => x.GetPersonalRelationships(It.IsAny<long>())).Returns(noMatchingRelationships);

            var response = _classUnderTest.Execute(request);

            response.PersonId.Equals(request.PersonId);

            response.PersonalRelationships.Parents.Should().BeEmpty();
            response.PersonalRelationships.Siblings.Should().BeEmpty();
            response.PersonalRelationships.Children.Should().BeEmpty();
            response.PersonalRelationships.Other.Should().BeEmpty();
        }

        [Test]
        public void WhenThereArePersonalRelationships_ReturnsListOfIDs()
        {
            var request = new GetRelationshipsRequest() { PersonId = 123456789 };
            PersonalRelationships matchingRelationships = _fixture.Create<PersonalRelationships>();
            _mockSocialCareGateway.Setup(x => x.GetPersonalRelationships(It.IsAny<long>())).Returns(matchingRelationships);

            var response = _classUnderTest.Execute(request);

            response.PersonId.Equals(request.PersonId);

            response.PersonalRelationships.Parents.Should().NotBeEmpty();
            response.PersonalRelationships.Siblings.Should().NotBeEmpty();
            response.PersonalRelationships.Children.Should().NotBeEmpty();
            response.PersonalRelationships.Other.Should().NotBeEmpty();
        }

        [Test]
        public void WhenThereAreSomePersonalRelationships_ReturnsListOfIDs()
        {
            var request = new GetRelationshipsRequest() { PersonId = 123456789 };
            var matchingRelationships = TestHelper.CreateRandomPersonalRelationship();
            _mockSocialCareGateway.Setup(x => x.GetPersonalRelationships(It.IsAny<long>())).Returns(matchingRelationships);

            var response = _classUnderTest.Execute(request);

            response.PersonId.Equals(request.PersonId);

            response.PersonalRelationships.Parents.Should().Equal(matchingRelationships.Parents);
            response.PersonalRelationships.Siblings.Should().Equal(matchingRelationships.Siblings);
            response.PersonalRelationships.Children.Should().Equal(matchingRelationships.Children);
            response.PersonalRelationships.Other.Should().Equal(matchingRelationships.Other);
        }
    }
}
