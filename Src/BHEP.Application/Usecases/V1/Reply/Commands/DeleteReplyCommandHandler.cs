using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Reply;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Reply.Commands;
public sealed class DeleteReplyCommandHandler : ICommandHandler<Command.DeleteReplyCommand>
{
    private readonly IReplyRepository ReplyRepository;

    public DeleteReplyCommandHandler(IReplyRepository ReplyRepository)
    {
        this.ReplyRepository = ReplyRepository;
    }

    public async Task<Result> Handle(Command.DeleteReplyCommand request, CancellationToken cancellationToken)
    {
        var Reply = await ReplyRepository.FindByIdAsync(request.Id, cancellationToken);
        if (Reply == null)
            return Result.Failure("Reply not found");

        try
        {
            ReplyRepository.Remove(Reply);

            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
