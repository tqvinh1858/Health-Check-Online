using System.Linq.Expressions;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IDeletionRequestRepository : IRepositoryBase<DeletionRequest, int>
{
    Task<DeletionRequest> FindByUserIdAsync(int UserId, CancellationToken cancellationToken = default, params Expression<Func<DeletionRequest, object>>[] includeProperties);
}
