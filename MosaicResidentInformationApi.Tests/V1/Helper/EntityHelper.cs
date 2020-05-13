using Bogus;
using MosaicResidentInformationApi.V1.Domain;

namespace MosaicResidentInformationApi.Tests.V1.Helper
{
    public class EntityHelper
    {
        public static Entity CreateEntity()
        {
            var faker = new Faker();
            var entity = new Entity
            {
                Id = faker.Random.Int(),
                CreatedAt = faker.Date.Past(),
            };

            return entity;
        }
    }
}
