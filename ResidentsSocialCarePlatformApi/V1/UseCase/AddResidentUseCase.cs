using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class AddResidentUseCase : IAddResidentUseCase
    {
        private IMosaicGateway _mosaicGateway;
        public AddResidentUseCase(IMosaicGateway mosaicGateway)
        {
            _mosaicGateway = mosaicGateway;
        }

        public ResidentInformation Execute(AddResidentRequest resident)
        {
            var residentInformation = _mosaicGateway.InsertResident(firstName: resident.FirstName, lastName: resident.LastName);

            return residentInformation.ToResponse();
        }
    }
}
