using Microsoft.EntityFrameworkCore;

namespace MosaicResidentInformationApi.V1.Infrastructure
{
    public class MosaicContext : DbContext
    {
        public MosaicContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<TelephoneNumber> TelephoneNumber { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
