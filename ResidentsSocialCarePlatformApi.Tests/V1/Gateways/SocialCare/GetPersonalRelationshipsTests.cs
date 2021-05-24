using FluentAssertions;
using NUnit.Framework;
using ResidentsSocialCarePlatformApi.Tests.V1.Helper;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;

namespace ResidentsSocialCarePlatformApi.Tests.V1.Gateways.SocialCare
{
    public class GetPersonalRelationships : DatabaseTests
    {
        private SocialCareGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new SocialCareGateway(SocialCareContext);
        }

        [Test]
        public void WhenThereAreNoPersonalRelationships_ReturnsEmptyListForAllCategories()
        {
            var response = _classUnderTest.GetPersonalRelationships(123456789);

            response.Parents.Should().BeEmpty();
            response.Siblings.Should().BeEmpty();
            response.Children.Should().BeEmpty();
            response.Other.Should().BeEmpty();
        }

        [Test]
        public void WhenPersonalRelationshipsIncludesParents_ReturnsIdsOfParents()
        {
            var (person, parent1, parent2) = AddRelationshipsForPersonToDatabase("Child's Parents");

            var response = _classUnderTest.GetPersonalRelationships(person.Id);

            response.Parents.Should().HaveCount(2);
            response.Parents.Should().Equal(parent1.Id, parent2.Id);
            response.Siblings.Should().BeEmpty();
            response.Children.Should().BeEmpty();
            response.Other.Should().BeEmpty();
        }

        [Test]
        public void WhenPersonalRelationshipsIncludesSiblings_ReturnsIdsOfSiblings()
        {
            var (person, sibling1, sibling2) = AddRelationshipsForPersonToDatabase("Child's Siblings");

            var response = _classUnderTest.GetPersonalRelationships(person.Id);

            response.Siblings.Should().HaveCount(2);
            response.Siblings.Should().Equal(sibling1.Id, sibling2.Id);
            response.Parents.Should().BeEmpty();
            response.Children.Should().BeEmpty();
            response.Other.Should().BeEmpty();
        }

        [Test]
        public void WhenPersonalRelationshipsIncludesChildren_ReturnsIdsOfChildren()
        {
            var (person, child1, child2) = AddRelationshipsForPersonToDatabase("Child's Children");

            var response = _classUnderTest.GetPersonalRelationships(person.Id);

            response.Children.Should().HaveCount(2);
            response.Children.Should().Equal(child1.Id, child2.Id);
            response.Parents.Should().BeEmpty();
            response.Siblings.Should().BeEmpty();
            response.Other.Should().BeEmpty();
        }

        [Test]
        public void WhenPersonalRelationshipsIncludesOther_ReturnsIdsOfOtherRelationships()
        {
            var (person, otherFamily1, otherFamily2) = AddRelationshipsForPersonToDatabase("Other Family Relationships");

            var response = _classUnderTest.GetPersonalRelationships(person.Id);

            response.Other.Should().HaveCount(2);
            response.Other.Should().Equal(otherFamily1.Id, otherFamily2.Id);
            response.Parents.Should().BeEmpty();
            response.Siblings.Should().BeEmpty();
            response.Children.Should().BeEmpty();
        }

        private (Person, Person, Person) AddRelationshipsForPersonToDatabase(string familyCategory)
        {
            var person = TestHelper.CreateDatabasePersonEntity(id: 1);
            var otherPerson1 = TestHelper.CreateDatabasePersonEntity(id: 2);
            var otherPerson2 = TestHelper.CreateDatabasePersonEntity(id: 3);
            var personalRelationshipType = TestHelper.CreatePersonalRelationshipType(familyCategory: familyCategory);
            var personalRelationship1 = TestHelper.CreatePersonalRelationship(
                personId: person.Id,
                personalRelTypeId: personalRelationshipType.PersonalRelationshipTypeId,
                otherPersonId: otherPerson1.Id
            );
            var personalRelationship2 = TestHelper.CreatePersonalRelationship(
                personId: person.Id,
                personalRelTypeId: personalRelationshipType.PersonalRelationshipTypeId,
                otherPersonId: otherPerson2.Id
            );

            SocialCareContext.Persons.Add(person);
            SocialCareContext.Persons.Add(otherPerson1);
            SocialCareContext.Persons.Add(otherPerson2);
            SocialCareContext.PersonalRelationshipTypes.Add(personalRelationshipType);
            SocialCareContext.PersonalRelationships.Add(personalRelationship1);
            SocialCareContext.PersonalRelationships.Add(personalRelationship2);

            SocialCareContext.SaveChanges();

            return (person, otherPerson1, otherPerson2);
        }
    }
}
