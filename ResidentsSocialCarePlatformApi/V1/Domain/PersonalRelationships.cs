using System.Collections.Generic;

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class PersonalRelationships
    {
        public List<long> Parents { get; set; }

        public List<long> Siblings { get; set; }

        public List<long> Children { get; set; }

        public List<long> Other { get; set; }
    }
}
