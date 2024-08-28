using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class PaymentVoucherRepository : IPaymentVoucherRepository
{
    private readonly ApplicationDbContext context;
    public PaymentVoucherRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
}
