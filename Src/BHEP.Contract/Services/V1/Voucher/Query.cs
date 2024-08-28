using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Voucher;
public static class Query
{
    public record GetVoucher(
        string? SearchTerm = null,
        string? SortColumn = null,
        string? SortOrder = null,
        int PageIndex = 1,
        int PageSize = 10
    );


    public record GetVoucherQuery(
        string? searchTerm,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize
      ) : IQuery<PagedResult<Responses.VoucherResponse>>;

    public record GetVoucherByIdQuery(int Id) : IQuery<Responses.VoucherResponse>;

    public record GetVoucherByUserIdQuery(
        int UserId,
        int? PageIndex = 1,
        int? PageSize = 10) : IQuery<PagedResult<Responses.VoucherResponse>>;
}
