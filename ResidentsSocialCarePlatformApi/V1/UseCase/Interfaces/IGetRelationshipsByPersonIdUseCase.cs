using System;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetRelationshipsByPersonIdUseCase
    {
        Relationships Execute(GetRelationshipsRequest request);
    }
}
