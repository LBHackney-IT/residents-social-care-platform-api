using System;

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class CaseNoteInformation
    {
        public string MosaicId { get; set; }

        public long CaseNoteId { get; set; }

        public long PersonVisitId { get; set; }

        public string NoteType { get; set; }

        public string CaseNoteTitle { get; set; }

        public string EffectiveDate { get; set; }

        public string CreatedOn { get; set; }

        public string LastUpdatedOn { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedByEmail { get; set; }

        public string LastUpdatedName { get; set; }

        public string LastUpdatedEmail { get; set; }

        public string CaseNoteContent { get; set; }

        public long RootCaseNoteId { get; set; }

        public string CompletedDate { get; set; }

        public string TimeoutDate { get; set; }

        public long CopyOfCaseNoteId { get; set; }

        public string CopiedByName { get; set; }

        public string CopiedByEmail { get; set; }

        public string CopiedDate { get; set; }
    }
}
