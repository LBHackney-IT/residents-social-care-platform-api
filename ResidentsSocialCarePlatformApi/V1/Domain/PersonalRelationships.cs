using System.Collections.Generic;

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class PersonalRelationships
    {
        public PersonalRelationships()
        {
            Parents = new List<long>();
            Siblings = new List<long>();
            Children = new List<long>();
            Other = new List<long>();
        }

        public List<long> Parents { get; set; } 

        public List<long> Siblings { get; set; }

        public List<long> Children { get; set; } 

        public List<long> Other { get; set; }
    }
}
