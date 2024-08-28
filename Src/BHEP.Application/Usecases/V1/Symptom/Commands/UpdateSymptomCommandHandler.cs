using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Symptom;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Symptom.Commands;
public sealed class UpdateSymptomCommandHandler : ICommandHandler<Command.UpdateSymptomCommand, Responses.SymptomResponse>
{
    private readonly ISymptomRepository SymptomRepository;
    private readonly IMapper mapper;

    public UpdateSymptomCommandHandler(ISymptomRepository SymptomRepository, IMapper mapper)
    {
        this.SymptomRepository = SymptomRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.SymptomResponse>> Handle(Command.UpdateSymptomCommand request, CancellationToken cancellationToken)
    {
        var Symptom = await SymptomRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new SymptomException.SymptomIdNotFoundException();

        try
        {
            Symptom.Update(request.Name, request.Description);
            SymptomRepository.Update(Symptom);

            var resultResponse = mapper.Map<Responses.SymptomResponse>(Symptom);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
