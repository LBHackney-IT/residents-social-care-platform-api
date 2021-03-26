using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

#nullable enable
namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetVisitInformationByVisitId : IGetVisitInformationByVisitId
    {
        private readonly ISocialCareGateway _socialCareGateway;

        public GetVisitInformationByVisitId(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public VisitInformation? Execute(long id)
        {
            var visitInformation = _socialCareGateway.GetVisitInformationByVisitId(id);
            return visitInformation?.ToResponse();
        }
    }
}
