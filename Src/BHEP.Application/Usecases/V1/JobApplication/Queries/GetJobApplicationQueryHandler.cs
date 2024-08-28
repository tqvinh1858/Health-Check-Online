using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;


namespace BHEP.Application.Usecases.V1.JobApplication.Queries;
public sealed class GetJobApplicationQueryHandler : IQueryHandler<Query.GetJobApplicationQuery, PagedResult<Responses.JobApplicationResponse>>
{
    private readonly IJobApplicationRepository JobApplicationRepository;
    private readonly IBlobStorageService blobStorageRepository;
    private readonly IMapper mapper;

    public GetJobApplicationQueryHandler(
        IJobApplicationRepository JobApplicationRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository)
    {
        this.JobApplicationRepository = JobApplicationRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<PagedResult<Responses.JobApplicationResponse>>> Handle(Query.GetJobApplicationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.UserEntities.JobApplication> EventsQuery;
            if (string.IsNullOrWhiteSpace(request.searchTerm))
            {
                EventsQuery = (request.status == null)
                    ? JobApplicationRepository.FindAll()
                    : JobApplicationRepository.FindAll(x => x.Status == request.status);
            }
            else
            {
                EventsQuery = (request.status == null)
                    ? JobApplicationRepository.FindAll(x => x.FullName.Contains(request.searchTerm))
                    : JobApplicationRepository.FindAll(x => x.FullName.Contains(request.searchTerm) && x.Status == request.status);
            }

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.UserEntities.JobApplication>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            foreach (var customer in Events.items)
            {
                if (!string.IsNullOrEmpty(customer.Avatar))
                    customer.Avatar = await blobStorageRepository.GetImageToBase64(customer.Avatar);
            }

            var result = mapper.Map<PagedResult<Responses.JobApplicationResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.UserEntities.JobApplication, object>> GetSortProperty(Query.GetJobApplicationQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "fullname" => e => e.FullName,
        "experienceyear" => e => e.ExperienceYear,
        _ => e => e.FullName
    };
}
