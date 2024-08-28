using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class CommentLike : EntityAuditBase<int>
{
    public int UserId { get; set; }
    public int CommentId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Comment Comment { get; set; } = null!;
}
