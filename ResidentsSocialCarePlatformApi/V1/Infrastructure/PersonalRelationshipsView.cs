using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure
{
    public class PersonalRelationshipsView
    {
        [Column("personal_relationship_id")]
        public long PersonalRelationshipId { get; set; }

        [Column("person_id")]
        public long PersonId { get; set; }

        [Column("other_person_id")]
        public long OtherPersonId { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("category")]
        public string Category { get; set; }
    }
}
