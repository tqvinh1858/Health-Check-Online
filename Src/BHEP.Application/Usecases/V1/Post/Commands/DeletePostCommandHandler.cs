using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Post;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Post.Commands;
public sealed class DeletePostCommandHandler : ICommandHandler<Command.DeletePostCommand>
{
    private readonly IPostRepository postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task<Result> Handle(Command.DeletePostCommand request, CancellationToken cancellationToken)
    {
        var postExist = await postRepository.FindByIdAsync(request.Id, cancellationToken);

        if (postExist == null)
            return Result.Failure("Post not found.");

        try
        {
            postRepository.Delete(postExist);

            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
