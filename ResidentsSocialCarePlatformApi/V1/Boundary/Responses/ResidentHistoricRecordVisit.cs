#nullable enable

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class ResidentHistoricRecordVisit : ResidentHistoricRecord
    {
        public VisitInformation Visit { get; set; } = null!;
    }
}
