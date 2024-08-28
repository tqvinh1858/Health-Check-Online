using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IServiceRateRepository : IRepositoryBase<ServiceRate, int>
{
    Task<bool> IsExistServiceRate(int userId, int transactionId, int serviceId);
    void Delete(ServiceRate ServiceRate);
    Task<float> GetAvgRating(int serviceId);
}
