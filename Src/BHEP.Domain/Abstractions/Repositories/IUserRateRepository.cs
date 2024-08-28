using System.Linq.Expressions;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IUserRateRepository : IRepositoryBase<UserRate, int>
{
    IQueryable<UserRate> FindAll(Expression<Func<UserRate, bool>>? predicate = null,
    params Expression<Func<UserRate, object>>[] includeProperties);
    Task<UserRate> FindByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<UserRate, object>>[] includeProperties);
    Task<UserRate> FindSingleAsync(Expression<Func<UserRate, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<UserRate, object>>[] includeProperties);
    Task<bool> IsExistUserRate(int customerId, int appointmentId, int employeeId);
    void Delete(UserRate userRate);
    Task<float> GetAvgRating(int doctorId);
}
