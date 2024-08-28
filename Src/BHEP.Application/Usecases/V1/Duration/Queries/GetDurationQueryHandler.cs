using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Duration;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Duration.Queries;
public sealed class GetDurationQueryHandler : IQueryHandler<Query.GetDurationQuery, PagedResult<Responses.DurationResponse>>
{
    private readonly IDurationRepository DurationRepository;
    private readonly IMapper mapper;

    public GetDurationQueryHandler(
        IDurationRepository DurationRepository,
        IMapper mapper)
    {
        this.DurationRepository = DurationRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.DurationResponse>>> Handle(Query.GetDurationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = DurationRepository.FindAll(x => x.UserId == request.UserId);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.SaleEntities.Duration>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            List<Responses.DurationResponse> listResponses = new List<Responses.DurationResponse>();
            foreach (var item in Events.items)
            {
                var response = new Responses.DurationResponse(
                    item.Id,
                    item.StartDate.ToString("dd-MM-yyyy"),
                    item.EndDate.ToString("dd-MM-yyyy"),
                    item.IsExpired,
                    mapper.Map<Contract.Services.V1.Service.Responses.ServiceResponse>(item.Service));

                listResponses.Add(response);
            }

            var result = PagedResult<Responses.DurationResponse>.Create(
                listResponses,
                Events.pageIndex,
                Events.pageSize,
                EventsQuery.Count());

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
