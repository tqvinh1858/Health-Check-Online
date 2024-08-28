using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IProductRateRepository : IRepositoryBase<ProductRate, int>
{
    Task<bool> IsExistProductRate(int userId, int transactionId, int productId);
    void Delete(ProductRate ProductRate);
    Task<float> GetAvgRating(int productId);
}
