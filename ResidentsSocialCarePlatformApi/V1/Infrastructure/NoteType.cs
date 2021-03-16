using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure
{
    [Table("dm_case_note_types", Schema = "dbo")]
    public class NoteType
    {
        [Column("note_type")]
        [MaxLength(16)]
        [Key]
        public string Type { get; set; }

        [Column("note_type_description")]
        [MaxLength(80)]
        public string Description { get; set; }
    }
}
