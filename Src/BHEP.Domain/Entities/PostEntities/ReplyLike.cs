using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class ReplyLike : EntityAuditBase<int>
{
    public int UserId { get; set; }
    public int ReplyId { get; set; }

    public virtual User User { get; set; }
    public virtual Reply Reply { get; set; }
}
