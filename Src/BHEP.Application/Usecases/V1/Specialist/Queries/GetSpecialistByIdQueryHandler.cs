using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Specialist;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using static BHEP.Contract.Services.V1.Symptom.Responses;


namespace BHEP.Application.Usecases.V1.Specialist.Queries;
public sealed class GetSpecialistByIdQueryHandler : IQueryHandler<Query.GetSpecialistByIdQuery, Responses.SpecialistGetByIdResponse>
{
    private readonly ISpecialistRepository SpecialistRepository;
    private readonly ISymptomRepository SymptomRepository;
    private readonly IMapper mapper;

    public GetSpecialistByIdQueryHandler(
        ISpecialistRepository SpecialistRepository,
        IMapper mapper,
        ISymptomRepository symptomRepository)
    {
        this.SpecialistRepository = SpecialistRepository;
        this.mapper = mapper;
        SymptomRepository = symptomRepository;
    }

    public async Task<Result<Responses.SpecialistGetByIdResponse>> Handle(Query.GetSpecialistByIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.AppointmentEntities.Specialist, object>>[] includeProperties = [x => x.Employees, x => x.SpecialistsSymptom];
        var result = await SpecialistRepository.FindByIdAsync(request.Id, includeProperties: includeProperties)
            ?? throw new SpecialistException.SpecialistIdNotFoundException();

        List<Domain.Entities.AppointmentEntities.Symptom> listSymp = new List<Domain.Entities.AppointmentEntities.Symptom>();
        foreach (var item in result.SpecialistsSymptom)
        {
            var symp = await SymptomRepository.FindByIdAsync(item.SymptomId);
            if (symp != null)
                listSymp.Add(symp);
        }
        var response = new Responses.SpecialistGetByIdResponse
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            Employees = mapper.Map<ICollection<Responses.EmployeeResponses>>(result.Employees),
            Symptoms = mapper.Map<ICollection<SymptomResponse>>(listSymp)
        };
        return Result.Success(response);
    }
}
