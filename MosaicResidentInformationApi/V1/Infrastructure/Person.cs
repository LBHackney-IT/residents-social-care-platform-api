using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MosaicResidentInformationApi.V1.Infrastructure
{
    [Table("dm_persons")]
    public class Person
    {
        [Column("person_id ")]
        [Key]
        public int Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("nhs_id")]
        public int NhsNumber { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("email_address")]
        public string EmailAddress { get; set; }
    }
}
