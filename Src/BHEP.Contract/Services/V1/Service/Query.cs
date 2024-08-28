using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Service;
public static class Query
{
    public record GetService(
       string? SearchTerm = null,
       ServiceType? ServiceType = null,
       string? SortColumn = null,
       string? SortOrder = null,
       int PageIndex = 1,
       int PageSize = 10);

    public record GetServiceQuery(
        string? searchTerm,
        ServiceType? serviceType,
        string? sortColumn,
        SortOrder? sortOrder,
        int pageIndex,
        int pageSize
       ) : IQuery<PagedResult<Responses.ServiceGetAllResponse>>;

    public record GetServiceByIdQuery(int Id) : IQuery<Responses.ServiceGetByIdResponse>;

    public record VnPayReturnQuery(
        bool? isGenerateCode,
        string? code,
     string vnp_TxnRef,
     string vnp_TransactionNo,
     string vnp_OrderInfo,
     string vnp_ResponseCode);


}
