using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class UserCode
{
    public int UserId { get; set; }
    public int CodeId { get; set; }

    public virtual User User { get; set; }
    public virtual Code Code { get; set; }
}
