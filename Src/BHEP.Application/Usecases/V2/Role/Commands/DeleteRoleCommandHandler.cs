using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V2.Role;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;

namespace BHEP.Application.Usecases.V2.Role.Commands;
public sealed class DeleteRoleCommandHandler : ICommandHandler<Command.DeleteRoleCommand>
{
    private readonly IRoleRepository roleRepository;
    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public async Task<Result> Handle(Command.DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExist = await roleRepository.GetByIdAsync(request.Id)
            ?? throw new RoleException.RoleIdNotFoundException();

        try
        {
            roleRepository.DeleteAsync(roleExist.Id);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
