using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.BlogEntities;
public class Topic : EntityBase<int>
{
    public Topic()
    {
        BlogTopics = new HashSet<BlogTopic>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<BlogTopic> BlogTopics { get; set; }

    public void Update(
        string Name,
        string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }
}
