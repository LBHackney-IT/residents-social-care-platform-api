namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class ResidentHistoricRecord
    {
        public long MosaicId { get; set; }

        public RecordType RecordType { get; set; }

        public string CaseNoteTitle { get; set; }
    }

    public enum RecordType
    {
        CaseNote,
        Visit
    }
}
