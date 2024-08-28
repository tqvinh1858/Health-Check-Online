using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Comment;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Comment.Commands;
public sealed class UpdateCommentCommandHandler : ICommandHandler<Command.UpdateCommentCommand, Responses.CommentResponse>
{
    private readonly ICommentRepository CommentRepository;

    public UpdateCommentCommandHandler(ICommentRepository CommentRepository)
    {
        this.CommentRepository = CommentRepository;
    }

    public async Task<Result<Responses.CommentResponse>> Handle(Command.UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var Comment = await CommentRepository.FindByIdAsync(request.Id.Value);
        if (Comment == null)
            return Result.Failure<Responses.CommentResponse>("Comment not found");

        try
        {
            Comment.Content = request.Content;
            CommentRepository.Update(Comment);

            var resultResponse = new Responses.CommentResponse(
               Comment.Id,
               Comment.UserId,
               Comment.PostId,
               Comment.Content);

            return Result.Success(resultResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
