using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Entities.BlogEntities;
public class Blog : EntityAuditBase<int>
{
    public Blog()
    {
        BlogTopics = new HashSet<BlogTopic>();
        BlogRates = new HashSet<BlogRate>();
        BlogPhotos = new HashSet<BlogPhoto>();
    }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int View { get; set; } = 0;
    public BlogStatus Status { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<BlogTopic> BlogTopics { get; set; }
    public virtual ICollection<BlogRate> BlogRates { get; set; }
    public virtual ICollection<BlogPhoto> BlogPhotos { get; set; }

    public void Update(
        string Title,
        string Content,
        BlogStatus Status)
    {
        this.Title = Title;
        this.Content = Content;
        this.Status = Status;
    }
}
