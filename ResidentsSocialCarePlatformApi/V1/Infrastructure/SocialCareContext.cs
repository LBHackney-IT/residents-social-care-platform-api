using Microsoft.EntityFrameworkCore;

namespace ResidentsSocialCarePlatformApi.V1.Infrastructure
{
    public class SocialCareContext : DbContext
    {
        public SocialCareContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TelephoneNumber> TelephoneNumbers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CaseNote> CaseNotes { get; set; }

        public DbSet<NoteType> NoteTypes { get; set; }

        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // composite primary key for TelephoneNumber table
            modelBuilder.Entity<TelephoneNumber>()
                .HasKey(telephoneNumber => new
                {
                    telephoneNumber.Id,
                    telephoneNumber.PersonId
                });
        }
    }
}
