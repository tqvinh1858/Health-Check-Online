using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.User.Commands;
public sealed class DeleteUserCommandHandler : ICommandHandler<Command.DeleteUserCommand>
{
    private readonly IUserRepository CustomerRepository;
    private readonly IRoleRepository roleRepository;
    public DeleteUserCommandHandler(
        IUserRepository CustomerRepository,
        IRoleRepository roleRepository)
    {
        this.CustomerRepository = CustomerRepository;
        this.roleRepository = roleRepository;
    }
    public async Task<Result> Handle(Command.DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var CustomerExist = await CustomerRepository.FindByIdAsync(request.Id)
            ?? throw new UserException.UserNotFoundException();
        // Can not delete Admin
        if (await roleRepository.IsAdmin(CustomerExist.RoleId))
            throw new UserException.UserBadRequestException("Can not delete Admin");

        try
        {
            CustomerRepository.Delete(CustomerExist);
            // Delete oldImage In BlobStorage
            //if (string.IsNullOrEmpty(oldImageUrl))
            //    blobStorageRepository.DeleteImageFromBlobStorage(oldImageUrl);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
