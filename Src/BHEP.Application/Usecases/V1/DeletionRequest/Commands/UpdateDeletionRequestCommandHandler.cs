using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.DeletionRequest.Commands;
public sealed class UpdateDeletionRequestCommandHandler : ICommandHandler<Command.UpdateDeletionRequestCommand, Responses.DeletionRequestResponse>
{
    private readonly IDeletionRequestRepository deletionRequestRepository;
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public UpdateDeletionRequestCommandHandler(
        IDeletionRequestRepository deletionRequestRepository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        this.deletionRequestRepository = deletionRequestRepository;
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Result<Responses.DeletionRequestResponse>> Handle(Command.UpdateDeletionRequestCommand request, CancellationToken cancellationToken)
    {
        var deletionRequestExist = await deletionRequestRepository.FindByIdAsync(request.Id, cancellationToken)
                   ?? throw new DeletionRequestException.DeletionRequestIdNotFoundException();

        var user = await userRepository.FindByIdAsync(request.UserId)
         ?? throw new DeletionRequestException.UserIdNotFoundException();

        try
        {
            deletionRequestExist.Update(
                request.UserId,
                request.Reason,
                request.Status,
                request.ProccessedDate);

            deletionRequestRepository.Update(deletionRequestExist);

            if (request.Status == DeletionRequestStatus.Approved)
            {
                user.IsDeleted = true;
                userRepository.Update(user);
            }

            var resultResponse = mapper.Map<Responses.DeletionRequestResponse>(deletionRequestExist);
            return Result.Success(resultResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
