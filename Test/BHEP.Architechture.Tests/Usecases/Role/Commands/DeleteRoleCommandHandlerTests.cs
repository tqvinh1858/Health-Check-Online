using BHEP.Application.Usecases.V1.Role.Commands;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using FakeItEasy;
using FluentAssertions;
using RoleEntity = BHEP.Domain.Entities.UserEntities.Role;


namespace BHEP.Architechture.Tests.Usecases.Role.Commands;
public class DeleteRoleCommandHandlerTests
{

    private readonly IRoleRepository roleRepository;
    private readonly DeleteRoleCommandHandler handler;


    public DeleteRoleCommandHandlerTests()
    {
        roleRepository = A.Fake<IRoleRepository>();
        handler = new DeleteRoleCommandHandler(roleRepository);
    }


    [Fact]
    public async Task Handle_WhenRoleIsDeleted_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = new Command.DeleteRoleCommand(1);
        var role = new RoleEntity { Id = 1, Name = "Admin", Description = "Administrator role" };

        A.CallTo(() => roleRepository.FindByIdAsync(command.Id, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult(role));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldThrowRoleIdNotFoundException_WhenRoleDoesNotExist()
    {
        // Arrange
        var command = new Command.DeleteRoleCommand(1);

        A.CallTo(() => roleRepository.FindByIdAsync(command.Id, A<CancellationToken>.Ignored))
            .Returns(Task.FromResult<RoleEntity>(null));

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleException.RoleIdNotFoundException>();
    }
}
