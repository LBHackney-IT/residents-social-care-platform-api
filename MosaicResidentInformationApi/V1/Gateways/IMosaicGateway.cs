using MosaicResidentInformationApi.V1.Domain;

namespace MosaicResidentInformationApi.V1.Gateways
{
    public interface IMosaicGateway
    {
        Entity GetEntityById(int id);
    }
}
