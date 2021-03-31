using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetVisitInformationByVisitId
    {
        VisitInformation Execute(long id);
    }
}
