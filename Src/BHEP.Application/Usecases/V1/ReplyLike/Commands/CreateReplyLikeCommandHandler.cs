using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ReplyLike;
using BHEP.Domain.Abstractions;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence.Repositories.Interface;

namespace BHEP.Application.Usecases.V1.ReplyLike.Commands;
public sealed class CreateReplyLikeCommandHandler : ICommandHandler<Command.CreateReplyLikeCommand, Responses.ReplyLikeResponse>
{
    private readonly IReplyLikeRepository ReplyLikeRepository;
    private readonly IUserRepository userRepository;
    private readonly IReplyRepository ReplyRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateReplyLikeCommandHandler(
        IReplyLikeRepository ReplyLikeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository,
        IReplyRepository ReplyRepository)
    {
        this.ReplyLikeRepository = ReplyLikeRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.ReplyRepository = ReplyRepository;
    }
    public async Task<Result<Responses.ReplyLikeResponse>> Handle(Command.CreateReplyLikeCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.IsExist(request.UserId))
            return Result.Failure<Responses.ReplyLikeResponse>("User not found");

        if (!await ReplyRepository.IsExist(request.ReplyId))
            return Result.Failure<Responses.ReplyLikeResponse>("Reply not found");

        if (await ReplyLikeRepository.IsExist(request.UserId, request.ReplyId))
            return Result.Failure<Responses.ReplyLikeResponse>("User already Like this Reply");

        try
        {
            var ReplyLike = new Domain.Entities.PostEntities.ReplyLike
            {
                UserId = request.UserId,
                ReplyId = request.ReplyId,
            };

            ReplyLikeRepository.Add(ReplyLike);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.ReplyLikeResponse>(ReplyLike);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
