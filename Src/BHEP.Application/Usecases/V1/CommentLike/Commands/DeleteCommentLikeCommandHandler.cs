using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CommentLike;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.CommentLike.Commands;
public sealed class DeleteReplyLikeCommandHandler : ICommandHandler<Command.DeleteCommentLikeCommand>
{
    private readonly ICommentLikeRepository CommentLikeRepository;
    public DeleteReplyLikeCommandHandler(
        ICommentLikeRepository CommentLikeRepository)
    {
        this.CommentLikeRepository = CommentLikeRepository;
    }
    public async Task<Result> Handle(Command.DeleteCommentLikeCommand request, CancellationToken cancellationToken)
    {
        var CommentLikeExist = await CommentLikeRepository.FindByIdAsync(request.Id);
        if (CommentLikeExist == null)
            return Result.Failure("CommentLike not found");

        try
        {
            CommentLikeRepository.Remove(CommentLikeExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
