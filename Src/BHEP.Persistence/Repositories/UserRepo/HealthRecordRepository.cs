using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Persistence.Repositories.UserRepo;
public class HealthRecordRepository : RepositoryBase<HealthRecord, int>, IHealthRecordRepository
{
    public HealthRecordRepository(ApplicationDbContext context) : base(context)
    {
    }
}
