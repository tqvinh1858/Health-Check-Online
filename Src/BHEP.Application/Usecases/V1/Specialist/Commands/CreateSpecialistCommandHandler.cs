using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Specialist;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.Specialist.Commands;
public sealed class CreateSpecialistCommandHandler : ICommandHandler<Command.CreateSpecialistCommand, Responses.SpecialistResponse>
{
    private readonly ISpecialistRepository SpecialistRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateSpecialistCommandHandler(
        ISpecialistRepository SpecialistRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.SpecialistRepository = SpecialistRepository;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.SpecialistResponse>> Handle(Command.CreateSpecialistCommand request, CancellationToken cancellationToken)
    {
        var SpecialistNameExist = SpecialistRepository.FindAll(x => x.Name == request.Name)
            ?? throw new SpecialistException.SpecialistBadRequestException("SpecialistName is already exist");

        var Specialist = new Domain.Entities.AppointmentEntities.Specialist
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            SpecialistRepository.Add(Specialist);
            await context.SaveChangesAsync();
            var resultResponse = mapper.Map<Responses.SpecialistResponse>(Specialist);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
