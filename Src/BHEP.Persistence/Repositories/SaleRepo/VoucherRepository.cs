using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Persistence.Repositories.SaleRepo;
public class VoucherRepository : RepositoryBase<Voucher, int>, IVoucherRepository
{
    public VoucherRepository(ApplicationDbContext context) : base(context)
    {
    }
}
