using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Auth;
public static class Command
{
    public record ForgetPasswordCommand(string Email, string NewPassword) : ICommand;
}
