using System.Linq.Expressions;
using BHEP.Contract.Constants;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.UserRepo;
public class UserRateRepository : RepositoryBase<UserRate, int>, IUserRateRepository
{
    private readonly ApplicationDbContext context;
    public UserRateRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public IQueryable<UserRate> FindAll(Expression<Func<UserRate, bool>>? predicate = null,
        params Expression<Func<UserRate, object>>[] includeProperties)
    {
        IQueryable<UserRate> items = context.Set<UserRate>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    public async Task<UserRate> FindByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<UserRate, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    public async Task<UserRate> FindSingleAsync(Expression<Func<UserRate, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<UserRate, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);

    public async Task<bool> IsExistUserRate(int customerId, int appointmentId, int employeeId)
        => await context.UserRate.FirstOrDefaultAsync(x =>
        x.CustomerId == customerId && x.AppointmentId == appointmentId && x.EmployeeId == employeeId) is null ? false : true;

    public void Delete(UserRate userRate)
    {
        userRate.DeletedDate = TimeZones.SoutheastAsia;
        userRate.IsDeleted = true;
        Update(userRate);
    }

    public async Task<float> GetAvgRating(int doctorId)
    {
        var rateList = await context.UserRate
        .Where(x => x.EmployeeId == doctorId)
        .Select(x => x.Rate)
        .ToListAsync();

        if (rateList.Any())
            return rateList.Average();

        return 0;
    }



}
