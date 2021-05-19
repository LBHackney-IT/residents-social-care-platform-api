using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure
{
    [Table("dm_personal_relationships", Schema = "dbo")]
    public class PersonalRelationship
    {
        [Column("personal_relationship_id")]
        [MaxLength(9)]
        [Key]
        public long PersonalRelationshipId { get; set; }

        [Column("person_id")]
        [MaxLength(16)]
        public long PersonId { get; set; }

        [Column("personal_rel_type_id")]
        [MaxLength(9)]
        public long PersonalRelTypeId { get; set; }

        [Column("other_person_id")]
        [MaxLength(16)]
        public long OtherPersonId { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Column("family_category")]
        [MaxLength(255)]
        public string FamilyCategory { get; set; }

        [Column("is_mother")]
        [MaxLength(1)]
        public string IsMother { get; set; }

        [Column("parental_responsibility")]
        [MaxLength(1)]
        public string ParentalReponsibility { get; set; }

        [Column("is_informal_carer")]
        [MaxLength(1)]
        public string IsInformalCarer { get; set; }
    }
}
