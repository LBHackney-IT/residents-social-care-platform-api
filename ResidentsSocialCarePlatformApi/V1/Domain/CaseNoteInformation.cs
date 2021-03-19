using System;

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class CaseNoteInformation
    {
        public string MosaicId { get; set; }

        public long CaseNoteId { get; set; }

        public long? PersonVisitId { get; set; }

        public string NoteType { get; set; }

        public string CaseNoteTitle { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedByEmail { get; set; }

        public string LastUpdatedName { get; set; }

        public string LastUpdatedEmail { get; set; }

        public string CaseNoteContent { get; set; }

        public long? RootCaseNoteId { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? TimeoutDate { get; set; }

        public long? CopyOfCaseNoteId { get; set; }

        public string CopiedByName { get; set; }

        public string CopiedByEmail { get; set; }

        public DateTime? CopiedDate { get; set; }
    }
}
