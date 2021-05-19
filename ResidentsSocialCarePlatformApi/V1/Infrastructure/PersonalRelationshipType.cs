using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure
{
    [Table("dm_personal_rel_types", Schema = "dbo")]
    public class PersonalRelationshipType
    {
        [Column("personal_rel_type_id")]
        [MaxLength(9)]
        [Key]
        public long PersonalRelationshipTypeId { get; set; }

        [Column("description")]
        [MaxLength(80)]
        public string Description { get; set; }

        [Column("family_category")]
        [MaxLength(255)]
        public string FamilyCategory { get; set; }

        [Column("is_informal_carer")]
        [MaxLength(1)]
        public string IsInformalCarer { get; set; }

        [Column("is_foster_and_adopt_sibling")]
        [MaxLength(1)]
        public string IsFosterAndAdoptSibling { get; set; }
    }
}
