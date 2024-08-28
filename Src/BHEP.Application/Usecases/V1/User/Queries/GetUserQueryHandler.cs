using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;

namespace BHEP.Application.Usecases.V1.User.Queries;
public sealed class GetUserQueryHandler : IQueryHandler<Query.GetUserQuery, PagedResult<Responses.UserGetAllResponse>>
{
    private readonly IUserRepository CustomerRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageRepository;
    public GetUserQueryHandler(
        IUserRepository CustomerRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository)
    {
        this.CustomerRepository = CustomerRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }
    public async Task<Result<PagedResult<Responses.UserGetAllResponse>>> Handle(Query.GetUserQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.UserEntities.User> EventsQuery;

        EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? CustomerRepository.FindAll(x => x.IsDeleted == false)
            : CustomerRepository.FindAll(x => x.FullName.Contains(request.searchTerm) && x.IsDeleted == false);

        EventsQuery = (request.RoleId == null && request.RoleId != 1) ? EventsQuery : EventsQuery.Where(x => x.RoleId == request.RoleId);

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.UserEntities.User>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        //foreach (var customer in Events.items)
        //{
        //    if (!string.IsNullOrEmpty(customer.Avatar))
        //        customer.Avatar = await blobStorageRepository.GetImageToBase64(customer.Avatar);
        //}

        var result = mapper.Map<PagedResult<Responses.UserGetAllResponse>>(Events);


        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.UserEntities.User, object>> GetSortProperty(Query.GetUserQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "fullname" => e => e.FullName,
        "createddate" => e => e.CreatedDate,
        _ => e => e.FullName
    };
}
