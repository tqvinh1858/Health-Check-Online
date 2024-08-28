using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class DeviceRepository : RepositoryBase<Device, int>, IDeviceRepository
{
    public DeviceRepository(ApplicationDbContext context) : base(context)
    {
    }
}
