using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;

namespace BHEP.Contract.Services.V1.GeoLocation;
public static class Query
{
    public record GetGeoLocationQuery() : IQuery<PagedResult<Responses.GeoLocationResponse>>;
    public record GetGeoLocationByIdQuery(int Id) : IQuery<Responses.GeoLocationResponse>;
}
