
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.GeoLocation;
public class Command
{
    public record CreateGeoLocationCommand(
        string Latitude,
        string Longitude) : ICommand<Responses.GeoLocationResponse>;

    public record UpdateGeoLocationCommand(
        int? Id,
        string Latitude,
        string Longitude) : ICommand<Responses.GeoLocationResponse>;

    public record DeleteGeoLocationCommand(int Id) : ICommand;
}
