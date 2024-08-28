using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.PostLike;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.PostLike.Commands;
public sealed class DeleteCommentLikeCommandHandler : ICommandHandler<Command.DeletePostLikeCommand>
{
    private readonly IPostLikeRepository postLikeRepository;
    public DeleteCommentLikeCommandHandler(
        IPostLikeRepository postLikeRepository)
    {
        this.postLikeRepository = postLikeRepository;
    }
    public async Task<Result> Handle(Command.DeletePostLikeCommand request, CancellationToken cancellationToken)
    {
        var PostLikeExist = await postLikeRepository.FindByIdAsync(request.Id)
            ?? throw new PostLikeException.PostLikeNotFoundException();

        try
        {
            postLikeRepository.Remove(PostLikeExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
