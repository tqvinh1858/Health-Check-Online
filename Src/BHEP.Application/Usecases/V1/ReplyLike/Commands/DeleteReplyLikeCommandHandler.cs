using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ReplyLike;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.ReplyLike.Commands;
public sealed class DeleteReplyLikeCommandHandler : ICommandHandler<Command.DeleteReplyLikeCommand>
{
    private readonly IReplyLikeRepository ReplyLikeRepository;
    public DeleteReplyLikeCommandHandler(
        IReplyLikeRepository ReplyLikeRepository)
    {
        this.ReplyLikeRepository = ReplyLikeRepository;
    }
    public async Task<Result> Handle(Command.DeleteReplyLikeCommand request, CancellationToken cancellationToken)
    {
        var ReplyLikeExist = await ReplyLikeRepository.FindByIdAsync(request.Id);
        if (ReplyLikeExist == null)
            return Result.Failure("ReplyLike not found");

        try
        {
            ReplyLikeRepository.Remove(ReplyLikeExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
