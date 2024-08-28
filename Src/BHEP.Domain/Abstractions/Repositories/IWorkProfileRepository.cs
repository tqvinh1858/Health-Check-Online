using System.Linq.Expressions;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IWorkProfileRepository : IRepositoryBase<WorkProfile, int>
{
    Task<WorkProfile> FindByUserIdAsync(int UserId, CancellationToken cancellationToken = default, params Expression<Func<WorkProfile, object>>[] includeProperties);
}
