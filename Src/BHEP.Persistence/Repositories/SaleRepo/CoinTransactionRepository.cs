using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class CoinTransactionRepository : RepositoryBase<CoinTransaction, int>, ICoinTransactionRepository
{
    private readonly ApplicationDbContext context;
    public CoinTransactionRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsExistProductTransaction(int transactionId, int productId)
    {
        var transaction = await context.CoinTransaction
            .Where(p => p.Id == transactionId)
            .Include(p => p.ProductTransactions.Where(o => o.TransactionId == transactionId && o.ProductId == productId))
            .FirstOrDefaultAsync();

        return transaction != null && transaction.ProductTransactions.Any();
    }

    public async Task<bool> IsExistServiceTransaction(int TransactionId, int serviceId)
    {
        var Transaction = await context.CoinTransaction
            .Where(p => p.Id == TransactionId)
            .Include(p => p.ServiceTransactions.Where(o => o.TransactionId == TransactionId && o.ServiceId == serviceId))
            .FirstOrDefaultAsync();

        return Transaction != null && Transaction.ServiceTransactions.Any();
    }

    public async Task<List<Service>> GetServices(int transactionId)
    {
        var services = await context.CoinTransaction.Where(x => x.Id == transactionId)
                                    .AsSplitQuery()
                                    .Include(x => x.ServiceTransactions).ThenInclude(x => x.Service)
                                    .SelectMany(x => x.ServiceTransactions)
                                    .Select(x => x.Service)
                                    .Distinct()
                                    .ToListAsync();
        return services;
    }
}
