using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.GeoLocation;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.GeoLocation.Commands;
public sealed class DeleteGeoLocationCommandHandler : ICommandHandler<Command.DeleteGeoLocationCommand>
{
    private readonly IGeoLocationRepository GeoLocationRepository;
    public DeleteGeoLocationCommandHandler(IGeoLocationRepository GeoLocationRepository)
    {
        this.GeoLocationRepository = GeoLocationRepository;
    }

    public async Task<Result> Handle(Command.DeleteGeoLocationCommand request, CancellationToken cancellationToken)
    {
        var GeoLocationExist = await GeoLocationRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new GeoLocationException.GeoLocationIdNotFoundException();

        try
        {
            GeoLocationRepository.Remove(GeoLocationExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
