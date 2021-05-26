using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetRelationshipsByPersonIdUseCase : IGetRelationshipsByPersonIdUseCase
    {
        ISocialCareGateway _socialCareGateway;

        public GetRelationshipsByPersonIdUseCase(ISocialCareGateway socialCareGateway)
        {
            _socialCareGateway = socialCareGateway;
        }

        public Relationships Execute(GetRelationshipsRequest request)
        {
            var personalRelationships = _socialCareGateway.GetPersonalRelationships(request.PersonId);

            return new Relationships()
            {
                PersonId = request.PersonId,
                PersonalRelationships = personalRelationships
            };
        }
    }
}
