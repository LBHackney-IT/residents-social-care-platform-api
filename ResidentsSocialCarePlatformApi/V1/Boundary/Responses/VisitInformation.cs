namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
#nullable enable
{
    public class VisitInformation
    {
        public long VisitId { get; set; }
        public long PersonId { get; set; }
        public string VisitType { get; set; } = null!;
        public string? PlannedDateTime { get; set; }
        public string? ActualDateTime { get; set; }
        public string? CreatedByName { get; set; }
        public string? CreatedByEmail { get; set; }
        public string? ReasonNotPlanned { get; set; }
        public string? ReasonVisitNotMade { get; set; }
        public bool SeenAloneFlag { get; set; }
        public bool CompletedFlag { get; set; }
    }
}
