using BHEP.Contract.Constants;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class PaymentRepository : RepositoryBase<Payment, int>, IPaymentRepository
{
    private readonly ApplicationDbContext context;
    public PaymentRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public void Delete(Payment payment)
    {
        payment.DeletedDate = TimeZones.SoutheastAsia;
        payment.IsDeleted = true;
        Update(payment);
    }
}
