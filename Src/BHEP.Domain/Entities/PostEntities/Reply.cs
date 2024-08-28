using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class Reply : EntityAuditBase<int>
{
    public Reply()
    {
        ReplyLikes = new HashSet<ReplyLike>();
    }
    public int UserId { get; set; }
    public int CommentId { get; set; }
    public int? ReplyParentId { get; set; }
    public string Content { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Comment Comment { get; set; } = null!;
    public virtual Reply? ReplyParent { get; set; } = null!;

    public virtual ICollection<ReplyLike> ReplyLikes { get; set; }
}
