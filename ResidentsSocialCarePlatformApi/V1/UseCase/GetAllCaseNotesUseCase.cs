using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetAllCaseNotesUseCase : IGetAllCaseNotesUseCase
    {
        private ISocialCareGateway _socialCareGateway;

        public GetAllCaseNotesUseCase(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public CaseNoteInformationList Execute(long personId)
        {
            var caseNotes = _socialCareGateway.GetAllCaseNotes(personId);

            return new CaseNoteInformationList
            {
                CaseNotes = caseNotes.ToResponse()
            };
        }
    }
}
