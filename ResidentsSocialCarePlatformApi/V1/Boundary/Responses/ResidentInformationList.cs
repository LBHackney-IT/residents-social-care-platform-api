using System.Collections.Generic;

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class ResidentInformationList
    {
        public List<ResidentInformation> Residents { get; set; }

        public string NextCursor { get; set; }
    }
}
