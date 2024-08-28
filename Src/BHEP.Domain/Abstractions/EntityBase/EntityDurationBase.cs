using BHEP.Contract.Constants;

namespace BHEP.Domain.Abstractions.EntityBase;
public abstract class EntityDurationBase<T> : EntityBase<T>, IDuration
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsExpired => EndDate < TimeZones.SoutheastAsia;
}
