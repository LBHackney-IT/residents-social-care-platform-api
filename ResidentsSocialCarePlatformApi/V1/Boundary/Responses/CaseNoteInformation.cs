namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class CaseNoteInformation
    {
        public string MosaicId { get; set; }
        public string CaseNoteId { get; set; }
        public string CaseNoteTitle { get; set; }
        public string CaseNoteContent { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedByEmail { get; set; }
        public string CreatedByName { get; set; }
        public string NoteType { get; set; }

    }
}
