using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.User.Responses;

namespace BHEP.Contract.Services.V1.User;
public static class Query
{
    public record GetUser(
        int? RoleId = null,
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10);
    public record GetUserQuery(
        int? RoleId,
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.UserGetAllResponse>>;

    public record GetDoctor(
        int? SpecialistId = null,
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10);


    public record GetDoctorQuery(
        int? SpecialistId,
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize) : IQuery<PagedResult<Responses.DoctorResponse>>;


    public record GetOutstandingDoctor(
        int PageIndex = 1,
        int PageSize = 10);


    public record GetOutstandingDoctorQuery(
       int pageIndex,
       int pageSize) : IQuery<PagedResult<Responses.DoctorResponse>>;



    public record GetUserByIdQuery(int Id) : IQuery<Responses.UserGetByIdResponse>;
}
