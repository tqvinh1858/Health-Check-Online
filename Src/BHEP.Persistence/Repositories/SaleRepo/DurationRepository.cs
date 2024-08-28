using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class DurationRepository : RepositoryBase<Duration, int>, IDurationRepository
{
    public DurationRepository(ApplicationDbContext context) : base(context)
    {
    }
}
