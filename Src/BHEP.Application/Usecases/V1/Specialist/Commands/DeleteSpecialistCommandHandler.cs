using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Specialist;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Specialist.Commands;
public sealed class DeleteSpecialistCommandHandler : ICommandHandler<Command.DeleteSpecialistCommand>
{
    private readonly ISpecialistRepository SpecialistRepository;
    public DeleteSpecialistCommandHandler(ISpecialistRepository SpecialistRepository)
    {
        this.SpecialistRepository = SpecialistRepository;
    }

    public async Task<Result> Handle(Command.DeleteSpecialistCommand request, CancellationToken cancellationToken)
    {
        var SpecialistExist = await SpecialistRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new SpecialistException.SpecialistIdNotFoundException();

        try
        {
            SpecialistRepository.Remove(SpecialistExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
