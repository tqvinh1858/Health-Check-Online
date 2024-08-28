using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Post;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.Post.Commands;
public sealed class CreateCommentCommandHandler : ICommandHandler<Command.CreatePostCommand, Responses.PostResponse>
{
    private readonly IPostRepository postRepository;
    private readonly IPostSpecialistRepository postSpecialistRepository;
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateCommentCommandHandler(
        IPostRepository postRepository,
        IUserRepository userRepository,
        IPostSpecialistRepository postSpecialistRepository,
        IUnitOfWork unitOfWork)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.postSpecialistRepository = postSpecialistRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Responses.PostResponse>> Handle(Command.CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Failure<Responses.PostResponse>("Customer not found.");

        try
        {
            var post = new Domain.Entities.PostEntities.Post
            {
                UserId = request.UserId,
                Title = request.Title,
                Content = request.Content,
                Age = request.Age,
                Gender = request.Gender,
                Status = Contract.Enumerations.PostStatus.Pending,
            };
            postRepository.Add(post);
            await unitOfWork.SaveChangesAsync();

            await postSpecialistRepository.AddMulti(
                request.Specialists.Select(SpecialistId => new Domain.Entities.PostEntities.PostSpecialist
                {
                    PostId = post.Id,
                    SpecialistId = SpecialistId
                }).ToList());

            var resultResponse = new Responses.PostResponse(
                       post.Id,
                       post.UserId,
                       post.Title,
                       post.Content,
                       post.Age,
                       post.Gender,
                       post.Status
                       );

            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
