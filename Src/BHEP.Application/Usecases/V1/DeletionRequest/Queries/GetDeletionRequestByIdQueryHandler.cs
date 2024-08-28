using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.DeletionRequest.Queries;
public sealed class GetDeletionRequestByIdQueryHandler : IQueryHandler<Query.GetDeletionRequestByIdQuery, Responses.DeletionRequestGetByIdResponse>
{
    private readonly IDeletionRequestRepository deletionRequestRepository;
    private readonly IMapper mapper;

    public GetDeletionRequestByIdQueryHandler(
        IDeletionRequestRepository deletionRequestRepository,
        IMapper mapper)
    {
        this.deletionRequestRepository = deletionRequestRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.DeletionRequestGetByIdResponse>> Handle(Query.GetDeletionRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await deletionRequestRepository.FindByIdAsync(request.Id)
          ?? throw new DeletionRequestException.DeletionRequestIdNotFoundException();

        var resultResponse = mapper.Map<Responses.DeletionRequestGetByIdResponse>(result);
        return Result.Success(resultResponse);
    }
}
