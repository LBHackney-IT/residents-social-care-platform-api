using System;

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class CaseNoteInformation
    {
        public string MosaicId { get; set; }

        public long CaseNoteId { get; set; }

        public int PersonVisitId { get; set; }

        public string NoteType { get; set; }

        public string CaseNoteTitle { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedByEmail { get; set; }

        public string LastUpdatedName { get; set; }

        public string LastUpdatedEmail { get; set; }

        public string CaseNoteContent { get; set; }

        public int RootCaseNoteId { get; set; }

        public DateTime CompletedDate { get; set; }

        public DateTime TimeoutDate { get; set; }

        public int CopyOfCaseNoteId { get; set; }

        public string CopiedBy { get; set; }

        public DateTime CopiedDate { get; set; }
    }
}
