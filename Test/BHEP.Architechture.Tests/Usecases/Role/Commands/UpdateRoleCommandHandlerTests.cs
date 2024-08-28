using AutoMapper;
using BHEP.Application.Usecases.V1.Role.Commands;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using RoleEntity = BHEP.Domain.Entities.UserEntities.Role;


namespace BHEP.Architechture.Tests.Usecases.Role.Commands;
public class UpdateRoleCommandHandlerTests
{
    private readonly IMapper mapper;
    private readonly IRoleRepository roleRepository;
    private readonly UpdateRoleCommandHandler handler;

    public UpdateRoleCommandHandlerTests()
    {
        mapper = A.Fake<IMapper>();
        roleRepository = A.Fake<IRoleRepository>();
        handler = new UpdateRoleCommandHandler(roleRepository, mapper);
    }

    [Fact]
    public async Task Handle_WhenRoleIsUpdated_ShouldReturnSuccesResult()
    {
        // Arrange
        var command = new Command.UpdateRoleCommand(1, "Updated Admin", "Updated Administrator role");
        var role = new RoleEntity { Id = 1, Name = "Admin", Description = "Administrator role" };
        var updatedRole = new Responses.RoleResponse ( 1,"Updated Admin","Updated Administrator role" );

        A.CallTo(() => roleRepository.FindByIdAsync(command.Id, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult(role));

        A.CallTo(() => mapper.Map<Responses.RoleResponse>(A<RoleEntity>.Ignored))
            .Returns(updatedRole);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.data.Should().Be(updatedRole);
    }


}
