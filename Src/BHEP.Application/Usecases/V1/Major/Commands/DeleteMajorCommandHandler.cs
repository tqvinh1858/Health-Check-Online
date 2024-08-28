using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Major;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Major.Commands;
public sealed class DeleteMajorCommandHandler : ICommandHandler<Command.DeleteMajorCommand>
{
    private readonly IMajorRepository majorRepository;

    public DeleteMajorCommandHandler(IMajorRepository majorRepository)
    {
        this.majorRepository = majorRepository;
    }

    public async Task<Result> Handle(Command.DeleteMajorCommand request, CancellationToken cancellationToken)
    {
        var majorExist = await majorRepository.FindByIdAsync(request.Id, cancellationToken)
          ?? throw new RoleException.RoleIdNotFoundException();

        try
        {
            majorRepository.Remove(majorExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
