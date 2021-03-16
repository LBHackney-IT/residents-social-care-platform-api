using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Gateways;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetAllCaseNotesUseCase
    {
        private ISocialCareGateway _socialCareGateway;

        public GetAllCaseNotesUseCase(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public CaseNoteInformationList Execute(long personId)
        {
            var caseNotes = _socialCareGateway.GetCaseNotes(personId);

            return new CaseNoteInformationList
            {
                CaseNotes = caseNotes.ToResponse()
            };
        }
    }
}
