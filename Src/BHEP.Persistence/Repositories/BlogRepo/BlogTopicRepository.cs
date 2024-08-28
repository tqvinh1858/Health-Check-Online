using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.BlogEntities;

namespace BHEP.Persistence.Repositories.BlogRepo;
public class BlogTopicRepository : IBlogTopicRepository
{
    private readonly ApplicationDbContext context;

    public BlogTopicRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Add(int blogId, ICollection<int> topics)
    {
        foreach (var topicId in topics)
        {
            await context.BlogTopic.AddAsync(new BlogTopic
            {
                BlogId = blogId,
                TopicId = topicId
            });
        }
    }


}
