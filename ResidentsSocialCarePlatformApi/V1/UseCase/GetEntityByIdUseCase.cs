using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;
using ResidentInformation = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;
using ResidentInformationResponse = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.ResidentInformation;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetEntityByIdUseCase : IGetEntityByIdUseCase
    {
        private ISocialCareGateway _socialCareGateway;
        public GetEntityByIdUseCase(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public ResidentInformation Execute(int id)
        {
            var residentInfo = _socialCareGateway.GetEntityById(id);
            if (residentInfo == null) throw new ResidentNotFoundException();

            return residentInfo.ToResponse();
        }
    }
}
