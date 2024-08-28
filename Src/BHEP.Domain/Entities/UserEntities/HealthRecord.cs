using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Entities.UserEntities;
public class HealthRecord : EntityBase<int>
{
    public int UserId { get; set; }
    public int DeviceId { get; set; }
    public float Temp { get; set; }
    public float HeartBeat { get; set; }
    public float ESpO2 { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Device Device { get; set; } = null!;
}
