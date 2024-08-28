using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Reply;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Reply.Commands;
public sealed class UpdateReplyCommandHandler : ICommandHandler<Command.UpdateReplyCommand, Responses.ReplyResponse>
{
    private readonly IReplyRepository ReplyRepository;

    public UpdateReplyCommandHandler(IReplyRepository ReplyRepository)
    {
        this.ReplyRepository = ReplyRepository;
    }

    public async Task<Result<Responses.ReplyResponse>> Handle(Command.UpdateReplyCommand request, CancellationToken cancellationToken)
    {
        var Reply = await ReplyRepository.FindByIdAsync(request.Id.Value);
        if (Reply == null)
            return Result.Failure<Responses.ReplyResponse>("Reply not found");

        try
        {
            Reply.Content = request.Content;
            ReplyRepository.Update(Reply);

            var resultResponse = new Responses.ReplyResponse(
               Reply.Id,
               Reply.UserId,
               Reply.CommentId,
               Reply.ReplyParentId,
               Reply.Content);

            return Result.Success(resultResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
