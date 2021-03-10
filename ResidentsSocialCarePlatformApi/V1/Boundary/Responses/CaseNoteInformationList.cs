namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class CaseNoteInformationList
    {
        public List<CaseNoteInformation> CaseNotes { get; set; }

        public string NextCursor { get; set; }
    }
}
