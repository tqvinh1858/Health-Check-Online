using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Comment;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Comment.Commands;
public sealed class CreateCommentCommandHandler : ICommandHandler<Command.CreateCommentCommand, Responses.CommentResponse>
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.postRepository = postRepository;
    }

    public async Task<Result<Responses.CommentResponse>> Handle(Command.CreateCommentCommand request, CancellationToken cancellationToken)
    {

        if (!await userRepository.IsExist(request.UserId))
            return Result.Failure<Responses.CommentResponse>("User not found");

        if (!await postRepository.IsExist(request.PostId))
            return Result.Failure<Responses.CommentResponse>("Post not found");

        var Comment = new Domain.Entities.PostEntities.Comment
        {
            UserId = request.UserId,
            PostId = request.PostId,
            Content = request.Content
        };

        try
        {
            commentRepository.Add(Comment);
            await unitOfWork.SaveChangesAsync();

            var resultResponse = new Responses.CommentResponse(
                Comment.Id,
                Comment.UserId,
                Comment.PostId,
                Comment.Content);

            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
