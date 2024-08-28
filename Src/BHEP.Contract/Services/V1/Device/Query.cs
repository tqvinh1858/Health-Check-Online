using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Device;
public static class Query
{
    public record GetDevice(
      string? SearchTerm = null,
      string? SortColumn = null,
      string? SortOrder = null,
      int PageIndex = 1,
      int PageSize = 10
      );


    public record GetDeviceQuery(
       string? searchTerm,
       string? sortColumn,
       SortOrder? sortOrder,
       int pageIndex,
       int pageSize
      ) : IQuery<PagedResult<Responses.DeviceResponse>>;

    public record GetDeviceByIdQuery(int Id) : IQuery<Responses.DeviceResponse>;

    public record GetDeviceByUserIdQuery(
        int UserId,
        int? PageIndex = 1,
        int? PageSize = 10) : IQuery<PagedResult<Responses.DeviceResponse>>;
}
