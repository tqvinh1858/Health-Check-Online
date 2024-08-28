using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;


namespace BHEP.Application.Usecases.V1.Role.Commands;
public sealed class DeleteRoleCommandHandler : ICommandHandler<Command.DeleteRoleCommand>
{
    private readonly IRoleRepository roleRepository;
    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public async Task<Result> Handle(Command.DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExist = await roleRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new RoleException.RoleIdNotFoundException();

        try
        {
            roleRepository.Remove(roleExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
