using System.ComponentModel.DataAnnotations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class ProductRate : EntityAuditBase<int>
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int TransactionId { get; set; }
    public string Reason { get; set; }
    [Range(0, 5)]
    public float Rate { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    public virtual CoinTransaction Transaction { get; set; } = null!;
}
