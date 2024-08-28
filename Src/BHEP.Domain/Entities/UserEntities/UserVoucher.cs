using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Entities.UserEntities;
public class UserVoucher
{
    public int UserId { get; set; }
    public int VoucherId { get; set; }

    public virtual User User { get; set; }
    public virtual Voucher Voucher { get; set; }
}
