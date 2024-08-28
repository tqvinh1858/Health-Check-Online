using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IPaymentRepository : IRepositoryBase<Payment, int>
{
    void Delete(Payment payment);
}
