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

        public DbSet<PersonalRelationships> PersonalRelationships { get; set; }

        public DbSet<PersonalRelationshipTypes> PersonalRelationshipTypes { get; set; }

        public DbSet<PersonalRelationshipsView> PersonalRelationshipsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // composite primary key for TelephoneNumber table
            modelBuilder.Entity<TelephoneNumber>()
                .HasKey(telephoneNumber => new
                {
                    telephoneNumber.Id,
                    telephoneNumber.PersonId
                });

            modelBuilder.Entity<PersonalRelationshipsView>()
                .ToView(name: "vw_personal_relationships", schema: "dbo")
                .HasKey(p => p.PersonalRelationshipId);
        }
    }
}
