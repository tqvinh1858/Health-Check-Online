namespace BHEP.Domain.Entities.SaleEntities;
public class VoucherTransaction
{
    public int TransactionId { get; set; }
    public int VoucherId { get; set; }

    public virtual CoinTransaction Transaction { get; set; }
    public virtual Voucher Voucher { get; set; }
}
