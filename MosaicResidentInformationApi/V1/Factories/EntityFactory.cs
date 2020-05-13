using MosaicResidentInformationApi.V1.Domain;
using MosaicResidentInformationApi.V1.Infrastructure;

namespace MosaicResidentInformationApi.V1.Factories
{
    public class EntityFactory : AbstractEntityFactory
    {
        public override Entity ToDomain(DatabaseEntity databaseEntity)
        {
            return new Entity
            {
                Id = databaseEntity.Id,
                CreatedAt = databaseEntity.CreatedAt,
            };
        }
    }
}
