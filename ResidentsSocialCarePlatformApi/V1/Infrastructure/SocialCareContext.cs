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

        public DbSet<Visit> Visits { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<PersonalRelationship> PersonalRelationships { get; set; }

        public DbSet<PersonalRelationshipType> PersonalRelationshipTypes { get; set; }

        public DbSet<PersonalRelationshipView> PersonalRelationshipsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // composite primary key for TelephoneNumber table
            modelBuilder.Entity<TelephoneNumber>()
                .HasKey(telephoneNumber => new
                {
                    telephoneNumber.Id,
                    telephoneNumber.PersonId
                });

            modelBuilder.Entity<PersonalRelationshipView>()
                .ToView(name: "vw_personal_relationships", schema: "dbo")
                .HasKey(p => p.PersonalRelationshipId);
        }
    }
}
