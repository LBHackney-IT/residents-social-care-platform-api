using System.Collections.Generic;
using System.Linq;
using MosaicResidentInformationApi.V1.Domain;
using MosaicResidentInformationApi.V1.Infrastructure;

namespace MosaicResidentInformationApi.V1.Factories
{
    public abstract class AbstractEntityFactory
    {
        public abstract Entity ToDomain(DatabaseEntity databaseEntity);

        public List<Entity> ToDomain(IEnumerable<DatabaseEntity> result)
        {
            return result.Select(ToDomain).ToList();
        }
    }
}
