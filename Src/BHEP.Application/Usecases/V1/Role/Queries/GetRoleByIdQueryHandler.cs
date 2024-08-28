using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;


namespace BHEP.Application.Usecases.V1.Role.Queries;
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
        var result = await roleRepository.FindByIdAsync(request.Id)
            ?? throw new RoleException.RoleIdNotFoundException();

        var resultResponse = mapper.Map<Responses.RoleResponse>(result);
        return Result.Success(resultResponse);
    }
}
