#nullable enable
namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class ResidentHistoricRecordCaseNote : ResidentHistoricRecord
    {
        public CaseNoteInformation CaseNote { get; set; } = null!;
    }
}
