using BHEP.Contract.Constants;

namespace BHEP.Domain.Abstractions.EntityBase;
public abstract class EntityAuditBase<T> : EntityBase<T>, IEntityAditBase<T>
{
    public DateTime CreatedDate { get; set; } = TimeZones.SoutheastAsia;
    public DateTime UpdatedDate { get; set; } = TimeZones.SoutheastAsia;
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
