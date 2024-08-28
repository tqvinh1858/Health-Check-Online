using System.Linq.Expressions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.UserRepo;
public class DeletionRequestRepository : RepositoryBase<DeletionRequest, int>, IDeletionRequestRepository
{
    public DeletionRequestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<DeletionRequest> FindByUserIdAsync(int UserId, CancellationToken cancellationToken = default, params Expression<Func<DeletionRequest, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsNoTracking().SingleOrDefaultAsync(x => x.UserId.Equals(UserId), cancellationToken);
}
