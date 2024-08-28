using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V2.Role;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;


namespace BHEP.Application.Usecases.V2.Role.Commands;
public sealed class UpdateRoleCommandHandler : ICommandHandler<Command.UpdateRoleCommand, Responses.RoleResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;

    public UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.RoleResponse>> Handle(Command.UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.Id)
            ?? throw new RoleException.RoleIdNotFoundException();

        try
        {
            role.Update(request.Name, request.Description);
            await roleRepository.UpdateAsync(role);

            var resultResponse = mapper.Map<Responses.RoleResponse>(role);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
