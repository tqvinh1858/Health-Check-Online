namespace BHEP.Domain.Entities.BlogEntities;
public class BlogTopic
{
    public int BlogId { get; set; }
    public int TopicId { get; set; }

    public virtual Blog Blog { get; set; } = null!;
    public virtual Topic Topic { get; set; } = null!;
}
