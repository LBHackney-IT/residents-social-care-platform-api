using Microsoft.EntityFrameworkCore;

namespace MosaicResidentInformationApi.V1.Infrastructure
{
    public interface IMosaicContext
    {
        DbSet<DatabaseEntity> DatabaseEntities { get; set; }
    }
}
