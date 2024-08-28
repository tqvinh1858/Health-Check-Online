using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class Comment : EntityAuditBase<int>
{
    public Comment()
    {
        CommentLikes = new HashSet<CommentLike>();
        Replies = new HashSet<Reply>();
    }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;

    public virtual ICollection<CommentLike> CommentLikes { get; set; }
    public virtual ICollection<Reply> Replies { get; set; }
}
