using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.GeoLocation;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.GeoLocation.Commands;
public sealed class CreateGeoLocationCommandHandler : ICommandHandler<Command.CreateGeoLocationCommand, Responses.GeoLocationResponse>
{
    private readonly IGeoLocationRepository GeoLocationRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateGeoLocationCommandHandler(
        IGeoLocationRepository GeoLocationRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.GeoLocationRepository = GeoLocationRepository;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.GeoLocationResponse>> Handle(Command.CreateGeoLocationCommand request, CancellationToken cancellationToken)
    {

        var GeoLocation = new Domain.Entities.UserEntities.GeoLocation
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };

        try
        {
            GeoLocationRepository.Add(GeoLocation);
            await context.SaveChangesAsync();
            var resultResponse = mapper.Map<Responses.GeoLocationResponse>(GeoLocation);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}

