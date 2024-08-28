using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Symptom;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Symptom.Commands;
public sealed class DeleteSymptomCommandHandler : ICommandHandler<Command.DeleteSymptomCommand>
{
    private readonly ISymptomRepository SymptomRepository;
    public DeleteSymptomCommandHandler(ISymptomRepository SymptomRepository)
    {
        this.SymptomRepository = SymptomRepository;
    }

    public async Task<Result> Handle(Command.DeleteSymptomCommand request, CancellationToken cancellationToken)
    {
        var SymptomExist = await SymptomRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new SymptomException.SymptomIdNotFoundException();

        try
        {
            SymptomRepository.Remove(SymptomExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
