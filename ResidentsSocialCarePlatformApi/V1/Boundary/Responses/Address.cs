using System;

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class Address
    {
        public DateTime? EndDate { get; set; }
        public string ContactAddressFlag { get; set; }
        public string DisplayAddressFlag { get; set; }

        /// <example>
        /// 4 Green Road
        /// </example>
        public string AddressLine1 { get; set; }
        /// <example>
        /// Hackney
        /// </example>
        public string AddressLine2 { get; set; }
        /// <example>
        /// London
        /// </example>
        public string AddressLine3 { get; set; }
        /// <example>
        /// E8 6TH
        /// </example>
        public string PostCode { get; set; }

    }
}
