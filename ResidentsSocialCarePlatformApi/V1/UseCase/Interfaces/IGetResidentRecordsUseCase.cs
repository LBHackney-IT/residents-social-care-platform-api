using System.Collections.Generic;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetResidentRecordsUseCase
    {
        List<ResidentHistoricRecord> Execute(long personId);
    }
}
