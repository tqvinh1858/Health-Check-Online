using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class Device : EntityBase<int>
{
    public Device()
    {
        HealthRecords = new HashSet<HealthRecord>();
    }
    public int ProductId { get; set; }
    public int? TransactionId { get; set; }
    public string Code { get; set; }
    public bool IsSale { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? SaleDate { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual CoinTransaction? Transaction { get; set; } = null!;
    public virtual ICollection<HealthRecord> HealthRecords { get; set; }

    public void Update(int productId, int? transactionId, string code, bool isSale, DateTime? saleDate)
    {
        ProductId = productId;
        TransactionId = transactionId;
        Code = code;
        IsSale = isSale;
        SaleDate = saleDate;
    }

}
