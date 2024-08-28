using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Topic;
public static class Query
{
    public record GetTopic(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageIndex,
        int pageSize);
    public record GetTopicQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.TopicResponse>>;
    public record GetTopicByIdQuery(int Id) : IQuery<Responses.TopicResponse>;
}
