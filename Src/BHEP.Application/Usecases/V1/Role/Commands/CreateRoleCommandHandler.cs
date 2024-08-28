using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence.Repositories.Interface;
using BHEP.Persistence.Repositories.UserRepo;



namespace BHEP.Application.Usecases.V1.Role.Commands;
public sealed class CreateRoleCommandHandler : ICommandHandler<Command.CreateRoleCommand, Responses.RoleResponse>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)

    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Command.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleExists = _roleRepository.FindAll(x => x.Name == request.Name).Any();

        if (roleExists)
        {
            throw new RoleException.RoleBadRequestException("RoleName is already exist");
        }

        var role = new Domain.Entities.UserEntities.Role
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            _roleRepository.Add(role);

            var resultResponse = _mapper.Map<Responses.RoleResponse>(role);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
