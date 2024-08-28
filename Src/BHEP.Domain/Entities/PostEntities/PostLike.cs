using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.PostEntities;
public class PostLike : EntityBase<int>
{
    public int UserId { get; set; }
    public int PostId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;
}
