using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class CoinTransaction : EntityAuditBase<int>
{
    public CoinTransaction()
    {
        VoucherTransaction = new HashSet<VoucherTransaction>();
        ProductTransactions = new HashSet<ProductTransaction>();
        ServiceTransactions = new HashSet<ServiceTransaction>();
        Devices = new HashSet<Device>();
    }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public bool IsMinus { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public CoinTransactionType Type { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Code Code { get; set; } = null!;
    public virtual ICollection<VoucherTransaction> VoucherTransaction { get; set; }
    public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }
    public virtual ICollection<ServiceTransaction> ServiceTransactions { get; set; }
    public virtual ICollection<Device>? Devices { get; set; }
}
