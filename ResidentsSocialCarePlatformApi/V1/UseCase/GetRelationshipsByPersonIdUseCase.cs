using System;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetRelationshipsByPersonIdUseCase : IGetRelationshipsByPersonIdUseCase
    {
        ISocialCareGateway _socialCareGateway;

        public GetRelationshipsByPersonIdUseCase(ISocialCareGateway socialCareGateways)
        {
            this._socialCareGateway = socialCareGateways;
        }

        public Relationships Execute(long personId)
        {
            var personalRelationships = _socialCareGateway.GetPersonalRelationships(personId);


            return new Relationships()
            {
                PersonId = personId,
                PersonalRelationships = personalRelationships
            };
        }
    }
}
