using BHEP.Contract.Constants;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class ProductRateRepository : RepositoryBase<ProductRate, int>, IProductRateRepository
{
    private readonly ApplicationDbContext context;
    public ProductRateRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExistProductRate(int userId, int transactionId, int productId)
        => await context.ProductRate.FirstOrDefaultAsync(x =>
        x.UserId == userId && x.TransactionId == transactionId && x.ProductId == productId) is null ? false : true;
    public void Delete(ProductRate ProductRate)
    {
        ProductRate.DeletedDate = TimeZones.SoutheastAsia;
        ProductRate.IsDeleted = true;
        Update(ProductRate);
    }

    public async Task<float> GetAvgRating(int productId)
    {
        var rateList = await context.ProductRate
        .Where(x => x.ProductId == productId)
        .Select(x => x.Rate)
        .ToListAsync();

        if (rateList.Any())
            return rateList.Average();

        return 0;
    }
}
