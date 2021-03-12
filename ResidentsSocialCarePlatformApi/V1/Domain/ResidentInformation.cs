using System;
using System.Collections.Generic;

namespace ResidentsSocialCarePlatformApi.V1.Domain
{
    public class ResidentInformation
    {
        public string MosaicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Uprn { get; set; }
        public string DateOfBirth { get; set; }
        public string AgeContext { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }

        public List<PhoneNumber> PhoneNumberList { get; set; }
        public List<Address> AddressList { get; set; }
        public string NhsNumber { get; set; }
        public string Restricted { get; set; }
    }

    public class PhoneNumber
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class Address
    {
        public DateTime? EndDate { get; set; }
        public string ContactAddressFlag { get; set; }
        public string DisplayAddressFlag { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostCode { get; set; }
    }
}
