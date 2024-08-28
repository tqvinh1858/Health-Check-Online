using System.Linq.Expressions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.UserRepo;
public class WorkProfileRepository : RepositoryBase<WorkProfile, int>, IWorkProfileRepository
{
    public WorkProfileRepository(ApplicationDbContext context) : base(context)
    {
    }


    public async Task<WorkProfile> FindByUserIdAsync(int UserId, CancellationToken cancellationToken = default, params Expression<Func<WorkProfile, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsNoTracking().SingleOrDefaultAsync(x => x.UserId.Equals(UserId), cancellationToken);
}
