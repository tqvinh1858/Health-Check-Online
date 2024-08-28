using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.User.Commands;
public sealed class ChangePasswordCommandHandler : ICommandHandler<Command.ChangePasswordCommand>
{
    private readonly IUserRepository userRepository;
    public ChangePasswordCommandHandler(
        IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<Result> Handle(Command.ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.Id.Value)
            ?? throw new UserException.UserNotFoundException();
        var checkPassword = userRepository.VerifyPassword(user.HashPassword, request.OldPassword);
        if (!checkPassword)
            throw new UserException.UserBadRequestException("Password not correct");
        try
        {
            var hashPassword = userRepository.HashPassword(request.NewPassword);
            user.HashPassword = hashPassword;
            user.UpdatedDate = TimeZones.SoutheastAsia;

            userRepository.Update(user);

            return Result.Success(201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
