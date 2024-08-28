
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BHEP.Contract.Services.V1.User;
public class Command
{
    public record CreateUserCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        Gender Gender) : ICommand<Responses.UserResponse>;

    public record UpdateUserCommand(
        int? Id,
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        IFormFile? Avatar) : ICommand<Responses.UserResponse>;

    public record ChangePasswordCommand(
        int? Id,
        string OldPassword,
        string NewPassword) : ICommand;

    public record DeleteUserCommand(int Id) : ICommand;
}
