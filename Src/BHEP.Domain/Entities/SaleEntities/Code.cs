using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class Code : EntityDurationBase<int>
{
    public Code()
    {
        UserCodes = new HashSet<UserCode>();
    }
    public int UserId { get; set; }
    public int TransactionId { get; set; }
    public string Name { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual CoinTransaction Transaction { get; set; } = null!;
    public virtual ICollection<UserCode> UserCodes { get; set; }
}
