using System.Linq.Expressions;
using AutoMapper;
using BHEP.Application.Usecases.V1.Role.Commands;
using BHEP.Contract.Services.V1.Role;
using BHEP.Domain.Exceptions;
using FakeItEasy;
using FluentAssertions;
using BHEP.Persistence.Repositories.Interface;
using RoleEntity = BHEP.Domain.Entities.UserEntities.Role;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Architechture.Tests.Usecases.V1.Role.Commands;

public class CreateRoleCommandHandlerTests
{
    private readonly IMapper mapper;
    private readonly IRoleRepository roleRepository;
    private readonly CreateRoleCommandHandler handler;

    public CreateRoleCommandHandlerTests()
    {
        mapper = A.Fake<IMapper>();
        roleRepository = A.Fake<IRoleRepository>();
        handler = new CreateRoleCommandHandler(roleRepository, mapper);
    }

    [Fact]
    public async Task Handle_WhenRoleIsCreated_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = new Command.CreateRoleCommand("Admin", "Administrator role");
        var role = new RoleEntity { Name = "Admin", Description = "Administrator role" };
        var roleResponse = new Responses.RoleResponse(1, "Admin", "Administrator role");

        A.CallTo(() => roleRepository.FindAll(A<Expression<Func<RoleEntity, bool>>>.Ignored))
            .Returns(Enumerable.Empty<RoleEntity>().AsQueryable());

        A.CallTo(() => mapper.Map<Responses.RoleResponse>(A<RoleEntity>.Ignored))
            .Returns(roleResponse);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.data.Should().Be(roleResponse);
    }


    [Fact]
    public async Task Handle_WhenRoleNameExists_ShouldThrowRoleBadRequestException()
    {
        // Arrange
        var command = new Command.CreateRoleCommand("Admin", "Administrator role");
        var existingRole = new RoleEntity { Name = "Admin", Description = "Administrator role" };

        A.CallTo(() => roleRepository.FindAll(A<Expression<Func<RoleEntity, bool>>>.Ignored))
            .Returns(new[] { existingRole }.AsQueryable());

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleException.RoleBadRequestException>()
            .WithMessage("RoleName is already exist");
    }

}

