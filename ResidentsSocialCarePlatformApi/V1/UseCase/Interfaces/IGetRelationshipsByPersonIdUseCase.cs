using System;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetRelationshipsByPersonIdUseCase
    {
        Relationships Execute(long personId);
    }
}
