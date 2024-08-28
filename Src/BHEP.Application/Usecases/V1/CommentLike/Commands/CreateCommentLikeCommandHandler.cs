using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CommentLike;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.CommentLike.Commands;
public sealed class CreateCommentLikeCommandHandler : ICommandHandler<Command.CreateCommentLikeCommand, Responses.CommentLikeResponse>
{
    private readonly ICommentLikeRepository CommentLikeRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository CommentRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateCommentLikeCommandHandler(
        ICommentLikeRepository CommentLikeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository,
        ICommentRepository CommentRepository)
    {
        this.CommentLikeRepository = CommentLikeRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.CommentRepository = CommentRepository;
    }
    public async Task<Result<Responses.CommentLikeResponse>> Handle(Command.CreateCommentLikeCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsExist(request.UserId))
            return Result.Failure<Responses.CommentLikeResponse>("User not found");

        if (!await CommentRepository.IsExist(request.CommentId))
            return Result.Failure<Responses.CommentLikeResponse>("Comment not found");

        if (await CommentLikeRepository.IsExist(request.UserId, request.CommentId))
            return Result.Failure<Responses.CommentLikeResponse>("User already Like this Comment");

        try
        {
            var CommentLike = new Domain.Entities.PostEntities.CommentLike
            {
                UserId = request.UserId,
                CommentId = request.CommentId,
            };

            CommentLikeRepository.Add(CommentLike);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.CommentLikeResponse>(CommentLike);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
