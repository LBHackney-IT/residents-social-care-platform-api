using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class AddResidentUseCase : IAddResidentUseCase
    {
        private ISocialCareGateway _socialCareGateway;
        public AddResidentUseCase(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public ResidentInformation Execute(AddResidentRequest resident)
        {
            var residentInformation = _socialCareGateway.InsertResident(firstName: resident.FirstName, lastName: resident.LastName);

            return residentInformation.ToResponse();
        }
    }
}
