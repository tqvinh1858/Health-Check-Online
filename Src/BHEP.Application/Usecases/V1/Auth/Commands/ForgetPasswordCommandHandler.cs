using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.Auth;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.Auth.Commands;
public sealed class ForgetPasswordCommandHandler : ICommandHandler<Command.ForgetPasswordCommand>
{
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;
    public ForgetPasswordCommandHandler(
        IUserRepository userRepository,
        ApplicationDbContext context)
    {
        this.userRepository = userRepository;
        this.context = context;
    }

    public async Task<Result> Handle(Command.ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindSingleAsync(x => x.Email == request.Email)
            ?? throw new AuthException.AuthEmailNotFoundException();

        try
        {
            var hashPassword = userRepository.HashPassword(request.NewPassword);
            user.HashPassword = hashPassword;
            user.UpdatedDate = TimeZones.SoutheastAsia;

            userRepository.Update(user);
            await context.SaveChangesAsync();

            return Result.Success(201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
}
