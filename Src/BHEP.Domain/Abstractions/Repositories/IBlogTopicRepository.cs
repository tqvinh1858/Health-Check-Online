namespace BHEP.Domain.Abstractions.Repositories;
public interface IBlogTopicRepository
{
    Task Add(int blogId, ICollection<int> topics);
}
