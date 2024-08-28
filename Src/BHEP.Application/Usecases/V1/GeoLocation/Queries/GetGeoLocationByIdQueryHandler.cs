using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.GeoLocation;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.GeoLocation.Queries;
public sealed class GetGeoLocationByIdQueryHandler : IQueryHandler<Query.GetGeoLocationByIdQuery, Responses.GeoLocationResponse>
{
    private readonly IGeoLocationRepository geoLocationRepository;
    private readonly IMapper mapper;

    public GetGeoLocationByIdQueryHandler(
        IMapper mapper,
        IGeoLocationRepository geoLocationRepository)
    {
        this.mapper = mapper;
        this.geoLocationRepository = geoLocationRepository;
    }

    public async Task<Result<Responses.GeoLocationResponse>> Handle(Query.GetGeoLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await geoLocationRepository.FindByIdAsync(request.Id)
    ?? throw new RoleException.RoleIdNotFoundException();

        var resultResponse = mapper.Map<Responses.GeoLocationResponse>(result);
        return Result.Success(resultResponse);
    }
}
