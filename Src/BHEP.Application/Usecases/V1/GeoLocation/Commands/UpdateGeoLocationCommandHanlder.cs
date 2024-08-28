using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.GeoLocation;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.GeoLocation.Commands;
public sealed class UpdateGeoLocationCommandHandler : ICommandHandler<Command.UpdateGeoLocationCommand, Responses.GeoLocationResponse>
{
    private readonly IGeoLocationRepository GeoLocationRepository;
    private readonly IMapper mapper;

    public UpdateGeoLocationCommandHandler(
        IGeoLocationRepository GeoLocationRepository,
        IMapper mapper)
    {
        this.GeoLocationRepository = GeoLocationRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.GeoLocationResponse>> Handle(Command.UpdateGeoLocationCommand request, CancellationToken cancellationToken)
    {
        var GeoLocation = await GeoLocationRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new GeoLocationException.GeoLocationIdNotFoundException();

        try
        {
            GeoLocation.Update(request.Latitude, request.Longitude);
            GeoLocationRepository.Update(GeoLocation);

            var resultResponse = mapper.Map<Responses.GeoLocationResponse>(GeoLocation);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

