using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class Payment : EntityAuditBase<int>
{

    public int UserId { get; set; }
    public string? TransactionId { get; set; }
    public string Method { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public virtual User User { get; set; } = null!;

    public void Update(
       string? TransactionId,
       string? Description,
       PaymentStatus Status,
       DateTime UpdatedDate)
    {
        this.TransactionId = TransactionId;
        this.Description = Description;
        this.Status = Status;
        this.UpdatedDate = UpdatedDate;
    }


}
