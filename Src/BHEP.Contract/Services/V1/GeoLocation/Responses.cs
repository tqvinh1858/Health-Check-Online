namespace BHEP.Contract.Services.V1.GeoLocation;

public static class Responses
{
    public record GeoLocationResponse(
        int Id,
        string Latitude,
        string Longitude);
}
