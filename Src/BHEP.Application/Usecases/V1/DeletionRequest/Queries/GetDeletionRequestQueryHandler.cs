using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.DeletionRequest.Queries;
public sealed class GetDeletionRequestQueryHandler : IQueryHandler<Query.GetDeletionRequestQuery, PagedResult<Responses.DeletionRequestResponse>>
{
    private readonly IDeletionRequestRepository deletionRequestRepository;
    private readonly IMapper mapper;

    public GetDeletionRequestQueryHandler(
        IDeletionRequestRepository deletionRequestRepository,
        IMapper mapper)
    {
        this.deletionRequestRepository = deletionRequestRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.DeletionRequestResponse>>> Handle(Query.GetDeletionRequestQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.UserEntities.DeletionRequest> EventsQuery;
            if (string.IsNullOrWhiteSpace(request.searchTerm))
            {
                EventsQuery = (request.status == null)
                    ? deletionRequestRepository.FindAll()
                    : deletionRequestRepository.FindAll(x => x.Status == request.status);
            }
            else
            {
                EventsQuery = (request.status == null)
                    ? deletionRequestRepository.FindAll(x => x.User.FullName.Contains(request.searchTerm))
                    : deletionRequestRepository.FindAll(x => x.User.FullName.Contains(request.searchTerm) && x.Status == request.status);
            }

            var keySelector = GetSortProperty(request);

            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            var Events = await PagedResult<Domain.Entities.UserEntities.DeletionRequest>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);


            List<Responses.DeletionRequestResponse> DeletionRequestGetAllResponses = new List<Responses.DeletionRequestResponse>();
            foreach (var item in Events.items)
            {
                var DeviceGetAllResponse = new Responses.DeletionRequestResponse(
                    item.Id,
                    item.UserId,
                    item.Reason,
                    item.Status,
                    item.CreatedDate,
                    item.ProccessedDate
                );

                DeletionRequestGetAllResponses.Add(DeviceGetAllResponse);
            }

            var result = PagedResult<Responses.DeletionRequestResponse>.Create(
                  DeletionRequestGetAllResponses,
                  Events.pageIndex,
                  Events.pageSize,
                  EventsQuery.Count()
                  );
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.UserEntities.DeletionRequest, object>> GetSortProperty(Query.GetDeletionRequestQuery request)
   => request.sortColumn?.ToLower() switch
   {
       "reason" => e => e.Reason,
       "name" => e => e.User.FullName,
       _ => e => e.User.FullName
   };
}
