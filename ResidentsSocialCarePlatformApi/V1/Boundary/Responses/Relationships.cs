using ResidentsSocialCarePlatformApi.V1.Domain;

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class Relationships
    {
        public long PersonId { get; set; }
        public PersonalRelationships PersonalRelationships { get; set; }
    }
}
