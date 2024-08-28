
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Role;
public class Command
{
    public record CreateRoleCommand(
        string Name,
        string Description) : ICommand<Responses.RoleResponse>;

    public record UpdateRoleCommand(
        int Id,
        string Name,
        string Description) : ICommand<Responses.RoleResponse>;

    public record DeleteRoleCommand(int Id) : ICommand;
}
