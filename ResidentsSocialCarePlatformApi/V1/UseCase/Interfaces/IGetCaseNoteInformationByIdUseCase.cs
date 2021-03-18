using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetCaseNoteInformationByIdUseCase
    {
        CaseNoteInformation Execute(long id);
    }
}
