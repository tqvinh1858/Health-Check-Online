using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;

namespace BHEP.Contract.Services.V1.Duration;
public static class Query
{
    public record GetDurationQuery(
        int UserId,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.DurationResponse>>;
}
