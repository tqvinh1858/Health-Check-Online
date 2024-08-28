using BHEP.Contract.Constants;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class ServiceRateRepository : RepositoryBase<ServiceRate, int>, IServiceRateRepository
{
    private readonly ApplicationDbContext context;
    public ServiceRateRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExistServiceRate(int userId, int transactionId, int serviceId)
        => await context.ServiceRate.FirstOrDefaultAsync(x =>
        x.UserId == userId && x.TransactionId == transactionId && x.ServiceId == serviceId) is null ? false : true;

    public void Delete(ServiceRate ServiceRate)
    {
        ServiceRate.DeletedDate = TimeZones.SoutheastAsia;
        ServiceRate.IsDeleted = true;
        Update(ServiceRate);
    }

    public async Task<float> GetAvgRating(int serviceId)
    {
        var rateList = await context.ServiceRate
        .Where(x => x.ServiceId == serviceId)
        .Select(x => x.Rate)
        .ToListAsync();

        if (rateList.Any())
            return rateList.Average();

        return 0;
    }
}
