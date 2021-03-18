using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.Infrastructure;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetCaseNoteInformationByIdUseCase : IGetCaseNoteInformationByIdUseCase
    {
        private ISocialCareGateway _socialCareGateway;

        public GetCaseNoteInformationByIdUseCase(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public CaseNoteInformation Execute(long id)
        {
            var caseNote = _socialCareGateway.GetCaseNoteInformationById(id);

            return caseNote?.ToResponse();
        }
    }

}
