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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // composite primary key for TelephoneNumber table
            modelBuilder.Entity<TelephoneNumber>()
                .HasKey(telephoneNumber => new
                {
                    telephoneNumber.Id,
                    telephoneNumber.PersonId
                });

            modelBuilder.Entity<CaseNote>()
                .HasOne(caseNote => caseNote.CreatedByWorker)
                .WithMany(worker => worker.CaseNotes)
                .HasForeignKey(caseNote => caseNote.CreatedBy)
                .HasPrincipalKey(worker => worker.SystemUserId);
        }
    }
}
