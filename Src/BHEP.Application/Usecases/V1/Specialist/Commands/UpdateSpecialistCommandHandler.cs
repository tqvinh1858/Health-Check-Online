using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Specialist;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Specialist.Commands;
public sealed class UpdateSpecialistCommandHandler : ICommandHandler<Command.UpdateSpecialistCommand, Responses.SpecialistResponse>
{
    private readonly ISpecialistRepository SpecialistRepository;
    private readonly IMapper mapper;

    public UpdateSpecialistCommandHandler(
        ISpecialistRepository SpecialistRepository,
        IMapper mapper)
    {
        this.SpecialistRepository = SpecialistRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.SpecialistResponse>> Handle(Command.UpdateSpecialistCommand request, CancellationToken cancellationToken)
    {
        var Specialist = await SpecialistRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new SpecialistException.SpecialistIdNotFoundException();

        try
        {
            Specialist.Update(request.Name, request.Description);
            SpecialistRepository.Update(Specialist);

            var resultResponse = mapper.Map<Responses.SpecialistResponse>(Specialist);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
