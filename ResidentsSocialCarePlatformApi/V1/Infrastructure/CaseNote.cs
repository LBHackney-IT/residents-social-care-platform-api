using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure
{
    [Table("case_notes", Schema = "dbo")]
    public class CaseNote
    {
        [Column("id")]
        [MaxLength(9)]
        [Key]
        public long Id { get; set; }

        [Column("person_id")]
        [MaxLength(9)]
        public long PersonId { get; set; }

        [Column("person_visit_id")]
        [MaxLength(9)]
        public long? PersonVisitId { get; set; }

        [Column("note_type")]
        [MaxLength(16)]
        public string NoteType { get; set; }

        [Column("title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Column("effective_date")]
        public DateTime? EffectiveDate { get; set; }

        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }

        [Column("last_updated_on")]
        public DateTime? LastUpdatedOn { get; set; }

        [Column("created_by")]
        [MaxLength(30)]
        public string CreatedBy { get; set; }

        [Column("last_updated_by")]
        [MaxLength(30)]
        public string LastUpdatedBy { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("root_case_note")]
        [MaxLength(9)]
        public long? RootCaseNoteId { get; set; }

        [Column("completed_date")]
        public DateTime? CompletedDate { get; set; }

        [Column("timeout_date")]
        public DateTime? TimeoutDate { get; set; }

        [Column("state")]
        [MaxLength(16)]
        public string State { get; set; }

        [Column("copy_of_case_note_id")]
        [MaxLength(9)]
        public long? CopyOfCaseNoteId { get; set; }

        [Column("copied_by")]
        [MaxLength(30)]
        public string CopiedBy { get; set; }

        [Column("copied_date")]
        public DateTime? CopiedDate { get; set; }

        //nav props
        public Worker Worker { get; set; }

        [ForeignKey("NoteType")]
        public NoteType NoteTypeDescription { get; set; }
    }
}
