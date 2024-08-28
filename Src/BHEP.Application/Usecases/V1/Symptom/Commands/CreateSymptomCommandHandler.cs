using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Symptom;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.Symptom.Commands;
public sealed class CreateSymptomCommandHandler : ICommandHandler<Command.CreateSymptomCommand, Responses.SymptomResponse>
{
    private readonly ISymptomRepository SymptomRepository;
    private readonly ISpecialistSymptomRepository specialistSymptomRepository;
    private readonly ISpecialistRepository specialistRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateSymptomCommandHandler(
        ISymptomRepository SymptomRepository,
        ApplicationDbContext context,
        IMapper mapper,
        ISpecialistSymptomRepository specialistSymptomRepository,
        ISpecialistRepository specialistRepository)
    {
        this.SymptomRepository = SymptomRepository;
        this.context = context;
        this.mapper = mapper;
        this.specialistSymptomRepository = specialistSymptomRepository;
        this.specialistRepository = specialistRepository;
    }

    public async Task<Result<Responses.SymptomResponse>> Handle(Command.CreateSymptomCommand request, CancellationToken cancellationToken)
    {
        var Specialist = await specialistRepository.FindByIdAsync(request.SpecialistId)
            ?? throw new SpecialistException.SpecialistIdNotFoundException();

        var SymptomNameExist = SymptomRepository.FindAll(x => x.Name == request.Name)
            ?? throw new SymptomException.SymptomBadRequestException("SymptomName is already exist");

        var Symptom = new Domain.Entities.AppointmentEntities.Symptom
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            SymptomRepository.Add(Symptom);
            await context.SaveChangesAsync();

            var specialistSymptom = new Domain.Entities.AppointmentEntities.SpecialistSymptom
            {
                SpecialistId = request.SpecialistId,
                SymptomId = Symptom.Id,
            };
            await specialistSymptomRepository.Add(specialistSymptom);

            var resultResponse = mapper.Map<Responses.SymptomResponse>(Symptom);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
