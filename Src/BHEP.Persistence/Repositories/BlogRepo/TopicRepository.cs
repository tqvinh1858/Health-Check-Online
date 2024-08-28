using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.BlogEntities;

namespace BHEP.Persistence.Repositories.BlogRepo;
public class TopicRepository : RepositoryBase<Topic, int>, ITopicRepository
{
    public TopicRepository(ApplicationDbContext context) : base(context)
    {
    }
}
