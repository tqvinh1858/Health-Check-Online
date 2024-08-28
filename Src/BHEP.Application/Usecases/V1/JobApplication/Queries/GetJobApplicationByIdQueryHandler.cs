using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;


namespace BHEP.Application.Usecases.V1.JobApplication.Queries;
public sealed class GetJobApplicationByIdQueryHandler : IQueryHandler<Query.GetJobApplicationByIdQuery, Responses.JobApplicationGetByIdResponse>
{
    private readonly IJobApplicationRepository JobApplicationRepository;
    private readonly IBlobStorageService blobStorageRepository;
    private readonly IMapper mapper;

    public GetJobApplicationByIdQueryHandler(
        IJobApplicationRepository JobApplicationRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository)
    {
        this.JobApplicationRepository = JobApplicationRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<Responses.JobApplicationGetByIdResponse>> Handle(Query.GetJobApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await JobApplicationRepository.FindByIdAsync(request.Id)
            ?? throw new JobApplicationException.JobApplicationIdNotFoundException();

        var resultResponse = mapper.Map<Responses.JobApplicationGetByIdResponse>(result);
        return Result.Success(resultResponse);
    }
}
