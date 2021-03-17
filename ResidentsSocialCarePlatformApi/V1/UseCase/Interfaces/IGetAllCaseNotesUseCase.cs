using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;

namespace ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces
{
    public interface IGetAllCaseNotesUseCase
    {
        CaseNoteInformationList Execute(long personId);
    }
}
