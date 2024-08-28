using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Appointment;
public static class Query
{
    public record GetAll(
        AppointmentStatus? Status,
        int? UserId,
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int PageIndex = 1,
        int PageSize = 10);

    public record GetByRange(
        double Range,
        string Latitude,
        string Longitude,
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10);

    public record GetAppointmentQuery(
        AppointmentStatus? Status,
        int? UserId,
        string? SearchTerm,
        string? SortColumn,
        SortOrder? SortOrder,
        int PageIndex,
        int PageSize) : IQuery<PagedResult<Responses.AppointmentGetAllResponse>>;


    public record GetAppointmentByIdQuery(int Id) : IQuery<Responses.AppointmentGetByIdResponse>;

    public record GetAppointmentInRangeQuery(
        double range,
        string latitude,
        string longitude,
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.AppointmentGetAllResponse>>;
}
