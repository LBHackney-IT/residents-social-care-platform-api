using System;

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class CaseNoteInformation
    {
        public string MosaicId { get; set; }
        public long CaseNoteId { get; set; }
        public string CaseNoteTitle { get; set; }
        public string CaseNoteContent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByEmail { get; set; }
        public string CreatedByName { get; set; }
        public string NoteType { get; set; }
    }
}
