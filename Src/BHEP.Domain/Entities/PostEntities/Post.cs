using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class Post : EntityAuditBase<int>
{
    public Post()
    {
        PostSpecialists = new List<PostSpecialist>();
        Comments = new HashSet<Comment>();
        PostLikes = new HashSet<PostLike>();
    }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public PostStatus Status { get; set; }


    public virtual User User { get; set; } = null!;
    public virtual ICollection<PostSpecialist> PostSpecialists { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<PostLike> PostLikes { get; set; }
}
