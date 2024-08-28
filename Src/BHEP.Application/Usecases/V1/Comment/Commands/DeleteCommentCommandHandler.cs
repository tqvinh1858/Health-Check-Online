using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Comment;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Comment.Commands;
public sealed class DeleteCommentCommandHandler : ICommandHandler<Command.DeleteCommentCommand>
{
    private readonly ICommentRepository CommentRepository;

    public DeleteCommentCommandHandler(ICommentRepository CommentRepository)
    {
        this.CommentRepository = CommentRepository;
    }

    public async Task<Result> Handle(Command.DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var Comment = await CommentRepository.FindByIdAsync(request.Id, cancellationToken);
        if (Comment == null)
            return Result.Failure("Comment not found");

        try
        {
            CommentRepository.Remove(Comment);

            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
