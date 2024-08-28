using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V2.Role;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;
using BHEP.Persistence;


namespace BHEP.Application.Usecases.V2.Role.Commands;
public sealed class CreateRoleCommandHandler : ICommandHandler<Command.CreateRoleCommand, Responses.RoleResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Command.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var isExistRole = await roleRepository.IsRoleExist(request.Name);
        if (isExistRole)
            throw new RoleException.RoleBadRequestException("RoleName is already exist");

        var role = new Domain.Entities.UserEntities.Role
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            await roleRepository.AddAsync(role);
            await context.SaveChangesAsync();
            var resultResponse = mapper.Map<Responses.RoleResponse>(role);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex);
        }
    }
}
