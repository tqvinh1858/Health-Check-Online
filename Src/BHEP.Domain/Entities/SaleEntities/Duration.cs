using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.SaleEntities;
public class Duration : EntityDurationBase<int>
{
    public int UserId { get; set; }
    public int ServiceId { get; set; }

    public virtual User User { get; set; }
    public virtual Service Service { get; set; }
}
