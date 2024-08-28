using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.BlogEntities;
public class BlogRate : EntityAuditBase<int>
{
    public int UserId { get; set; }
    public int BlogId { get; set; }
    public float Rate { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Blog Blog { get; set; } = null!;
}
