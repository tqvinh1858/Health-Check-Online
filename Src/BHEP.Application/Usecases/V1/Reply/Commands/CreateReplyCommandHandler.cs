using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Reply;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;
namespace BHEP.Application.Usecases.V1.Reply.Commands;
public sealed class CreateReplyCommandHandler : ICommandHandler<Command.CreateReplyCommand, Responses.ReplyResponse>
{
    private readonly IReplyRepository ReplyRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;

    public CreateReplyCommandHandler(
        IReplyRepository ReplyRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ICommentRepository commentRepository)
    {
        this.ReplyRepository = ReplyRepository;
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.commentRepository = commentRepository;
    }

    public async Task<Result<Responses.ReplyResponse>> Handle(Command.CreateReplyCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Failure<Responses.ReplyResponse>("User not found");
        var comment = await commentRepository.FindByIdAsync(request.CommentId);
        if (comment == null)
            return Result.Failure<Responses.ReplyResponse>("Comment not found");

        if (request.ReplyParentId != null)
        {
            var replyParent = await ReplyRepository.FindByIdAsync(request.ReplyParentId.Value);
            if (replyParent == null)
                return Result.Failure<Responses.ReplyResponse>("ReplyParent not found");
        }

        var Reply = new Domain.Entities.PostEntities.Reply
        {
            UserId = request.UserId,
            CommentId = request.CommentId,
            ReplyParentId = request.ReplyParentId is null ? null : request.ReplyParentId,
            Content = request.Content
        };

        try
        {
            ReplyRepository.Add(Reply);
            await unitOfWork.SaveChangesAsync();

            var resultResponse = new Responses.ReplyResponse(
                Reply.Id,
                Reply.UserId,
                Reply.CommentId,
                Reply.ReplyParentId,
                Reply.Content);

            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
