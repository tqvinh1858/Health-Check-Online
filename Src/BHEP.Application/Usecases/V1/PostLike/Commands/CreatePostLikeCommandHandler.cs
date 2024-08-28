using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.PostLike;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.PostLike.Commands;
public sealed class CreateCommentLikeCommandHandler : ICommandHandler<Command.CreatePostLikeCommand, Responses.PostLikeResponse>
{
    private readonly IPostLikeRepository PostLikeRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateCommentLikeCommandHandler(
        IPostLikeRepository PostLikeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        this.PostLikeRepository = PostLikeRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }
    public async Task<Result<Responses.PostLikeResponse>> Handle(Command.CreatePostLikeCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsExist(request.UserId))
            throw new PostLikeException.UserNotFoundException();

        if (!await postRepository.IsExist(request.PostId))
            throw new PostLikeException.PostNotFoundException();

        if (await PostLikeRepository.IsExist(request.UserId, request.PostId))
            throw new PostLikeException.PostLikeBadRequestException("User already Like this Post");

        try
        {
            var PostLike = new Domain.Entities.PostEntities.PostLike
            {
                UserId = request.UserId,
                PostId = request.PostId,
            };

            PostLikeRepository.Add(PostLike);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.PostLikeResponse>(PostLike);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
