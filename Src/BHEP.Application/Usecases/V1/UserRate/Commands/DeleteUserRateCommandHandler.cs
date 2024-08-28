using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.UserRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.UserRate.Commands;
public sealed class DeletePostLikeCommandHandler : ICommandHandler<Command.DeleteUserRateCommand>
{
    private readonly IUserRateRepository UserRateRepository;
    public DeletePostLikeCommandHandler(
        IUserRateRepository UserRateRepository)
    {
        this.UserRateRepository = UserRateRepository;
    }
    public async Task<Result> Handle(Command.DeleteUserRateCommand request, CancellationToken cancellationToken)
    {
        var UserRateExist = await UserRateRepository.FindByIdAsync(request.Id)
            ?? throw new UserRateException.UserRateIdNotFoundException();

        try
        {
            UserRateRepository.Delete(UserRateExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
