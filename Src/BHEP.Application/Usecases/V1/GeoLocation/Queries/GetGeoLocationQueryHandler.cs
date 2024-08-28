using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.GeoLocation;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.GeoLocation.Queries;
public sealed class GetGeoLocationQueryHandler : IQueryHandler<Query.GetGeoLocationQuery, PagedResult<Responses.GeoLocationResponse>>
{
    private readonly IGeoLocationRepository GeoLocationRepository;
    private readonly IMapper mapper;

    public GetGeoLocationQueryHandler(
        IGeoLocationRepository GeoLocationRepository,
        IMapper mapper)
    {
        this.GeoLocationRepository = GeoLocationRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.GeoLocationResponse>>> Handle(Query.GetGeoLocationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var EventsQuery = GeoLocationRepository.FindAll();

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.UserEntities.GeoLocation>.CreateAsync(EventsQuery, 1, 10);

            var result = mapper.Map<PagedResult<Responses.GeoLocationResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
