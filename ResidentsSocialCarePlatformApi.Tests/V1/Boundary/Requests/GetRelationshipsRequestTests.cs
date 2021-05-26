using Bogus;
using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Boundary.Request
{
    [TestFixture]
    public class GetRelationshipsRequestTests
    {
        private GetRelationshipsRequestValidator _classUnderTest;
        private Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
            _classUnderTest = new GetRelationshipsRequestValidator();
        }

        [Test]
        public void WhenPersonIdIsInvalid_ReturnsAnError()
        {
            var badGetRelationshipsRequest = new GetRelationshipsRequest() { PersonId = 0 };

            var response = _classUnderTest.Validate(badGetRelationshipsRequest);

            response.Should().NotBeNull();
            response.IsValid.Should().Be(false);
            response.ToString().Should().Be("Person ID must be greater than 0");
        }

        [Test]
        public void WhenPersonIdIsProvided_ReturnsItIsValid()
        {
            var validGetRelationshipsRequest = new GetRelationshipsRequest() { PersonId = 123456789 };

            var response = _classUnderTest.Validate(validGetRelationshipsRequest);

            response.Should().NotBeNull();
            response.IsValid.Should().Be(true);
        }
    }
}
