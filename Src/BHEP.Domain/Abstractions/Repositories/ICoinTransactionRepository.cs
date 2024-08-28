using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface ICoinTransactionRepository : IRepositoryBase<CoinTransaction, int>
{
    Task<bool> IsExistProductTransaction(int transactionId, int productId);
    Task<bool> IsExistServiceTransaction(int TransactionId, int serviceId);
    Task<List<Service>> GetServices(int transactionId);
}
