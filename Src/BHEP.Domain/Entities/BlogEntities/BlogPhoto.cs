using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.BlogEntities;
public class BlogPhoto : EntityBase<int>
{
    public int BlogId { get; set; }
    public string Image { get; set; }
    public int ONum { get; set; }

    public virtual Blog Blog { get; set; }
}
