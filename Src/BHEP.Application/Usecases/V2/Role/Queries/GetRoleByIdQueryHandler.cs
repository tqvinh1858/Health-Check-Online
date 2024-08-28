using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V2.Role;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;


namespace BHEP.Application.Usecases.V2.Role.Queries;
public sealed class GetRoleByIdQueryHandler : IQueryHandler<Query.GetRoleByIdQuery, Responses.RoleResponse>
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;

    public GetRoleByIdQueryHandler(
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.RoleResponse>> Handle(Query.GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await roleRepository.GetByIdAsync(request.Id)
            ?? throw new RoleException.RoleIdNotFoundException();

        Responses.RoleResponse response = result;
        return Result.Success(response);
    }
}
