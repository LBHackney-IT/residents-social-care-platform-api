using System.Collections.Generic;
using System.Linq;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;
using VisitInformation = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.VisitInformation;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetVisitInformationByPersonId : IGetVisitInformationByPersonId
    {
        private ISocialCareGateway _socialCareGateway;

        public GetVisitInformationByPersonId(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public List<VisitInformation> Execute(long id)
        {
            return _socialCareGateway
                .GetVisitInformationByPersonId(id)
                .Select(visit => visit.ToResponse())
                .ToList();
        }
    }
}
