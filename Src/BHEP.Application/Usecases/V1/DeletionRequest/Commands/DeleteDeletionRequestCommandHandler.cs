using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.DeletionRequest.Commands;
public sealed class DeleteDeletionRequestCommandHandler : ICommandHandler<Command.DeleteDeletionRequestCommand>
{
    private readonly IDeletionRequestRepository deletionRequestRepository;

    public DeleteDeletionRequestCommandHandler(
        IDeletionRequestRepository deletionRequestRepository)
    {
        this.deletionRequestRepository = deletionRequestRepository;
    }

    public async Task<Result> Handle(Command.DeleteDeletionRequestCommand request, CancellationToken cancellationToken)
    {
        var deletionRequestExist = await deletionRequestRepository.FindByIdAsync(request.Id, cancellationToken)
                           ?? throw new DeletionRequestException.DeletionRequestIdNotFoundException();

        try
        {
            deletionRequestRepository.Remove(deletionRequestExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
