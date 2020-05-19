using MosaicResidentInformationApi.V1.Domain;
using MosaicResidentInformationApi.V1.Factories;
using MosaicResidentInformationApi.V1.Infrastructure;

namespace MosaicResidentInformationApi.V1.Gateways
{
    public class MosaicGateway : IMosaicGateway
    {
        private readonly MosaicContext _mosaicContext;
        private readonly EntityFactory _entityFactory;

        public MosaicGateway(MosaicContext mosaicContext)
        {
            _mosaicContext = mosaicContext;
            _entityFactory = new EntityFactory();
        }

        public ResidentInformation GetEntityById(int id)
        {
            var result = _mosaicContext.DatabaseEntities.Find(id);

            return (result != null) ?
                _entityFactory.ToDomain(result) :
                null;
        }
    }
}
