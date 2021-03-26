using System.Collections.Generic;
using VisitInformation = ResidentsSocialCarePlatformApi.V1.Boundary.Responses.VisitInformation;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetVisitInformationByPersonId
    {
        List<VisitInformation> Execute(long id);
    }
}
