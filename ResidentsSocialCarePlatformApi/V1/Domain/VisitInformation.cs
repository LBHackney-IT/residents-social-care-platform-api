using System;
#nullable enable

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class VisitInformation
    {
        public long VisitId { get; set; }

        public string VisitType { get; set; } = null!;

        public DateTime? PlannedDateTime { get; set; }

        public DateTime? ActualDateTime { get; set; }

        public string? CreatedByName { get; set; }

        public string? CreatedByEmail { get; set; }

        public string? ReasonNotPlanned { get; set; }

        public string? ReasonVisitNotMade { get; set; }

        public string? SeenAloneFlag { get; set; }

        public string? CompletedFlag { get; set; }

        public long? CpRegistrationId { get; set; }

        public long? CpVisitScheduleStepId { get; set; }

        public long? CpVisitScheduleDays { get; set; }

        public string? CpVisitOnTime { get; set; }
    }
}
