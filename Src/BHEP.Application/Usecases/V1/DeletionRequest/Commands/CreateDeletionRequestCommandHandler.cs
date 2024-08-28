using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.DeletionRequest.Commands;
public sealed class CreateDeletionRequestCommandHandler : ICommandHandler<Command.CreateDeletionRequestCommand, Responses.DeletionRequestResponse>
{
    private readonly IDeletionRequestRepository deletionRequestRepository;
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public CreateDeletionRequestCommandHandler(
        IDeletionRequestRepository deletionRequestRepository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        this.deletionRequestRepository = deletionRequestRepository;
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Result<Responses.DeletionRequestResponse>> Handle(Command.CreateDeletionRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId)
           ?? throw new DeletionRequestException.UserIdNotFoundException();

        var deletionRequestExist = await deletionRequestRepository.FindByIdAsync(request.UserId)
            ?? throw new DeletionRequestException.DeletionRequestBadRequestException("Deletion Request already exists.");

        var deletionRequest = new Domain.Entities.UserEntities.DeletionRequest
        {
            UserId = request.UserId,
            Reason = request.Reason,
            Status = DeletionRequestStatus.Pending,
            CreatedDate = TimeZones.SoutheastAsia
        };

        try
        {
            deletionRequestRepository.Add(deletionRequest);
            var resultResponse = mapper.Map<Responses.DeletionRequestResponse>(deletionRequest);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
