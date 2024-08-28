using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;
using static BHEP.Contract.Services.V1.Symptom.Responses;
using static BHEP.Contract.Services.V1.User.Responses;


namespace BHEP.Application.Usecases.V1.Appointment.Queries;
public sealed class GetAppointmentByIdQueryHandler : IQueryHandler<Query.GetAppointmentByIdQuery, Responses.AppointmentGetByIdResponse>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly ISymptomRepository symptomRepository;
    private readonly IMapper mapper;

    public GetAppointmentByIdQueryHandler(
        IAppointmentRepository AppointmentRepository,
        IMapper mapper,
        ISymptomRepository symptomRepository)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.mapper = mapper;
        this.symptomRepository = symptomRepository;
    }

    public async Task<Result<Responses.AppointmentGetByIdResponse>> Handle(Query.GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.AppointmentEntities.Appointment, object>>[] includeProperties =
            [
                x => x.Customer,
                x => x.Employee,
                x => x.AppointmentSymptoms
            ];

        var appointment = await AppointmentRepository.FindSingleAsync(
            predicate: x => x.IsDeleted == false && x.Id == request.Id,
            cancellationToken,
            includeProperties);
        if (appointment is null)
            return Result.Failure<Responses.AppointmentGetByIdResponse>("Appointment not found");

        var symptoms = new List<Domain.Entities.AppointmentEntities.Symptom>();
        foreach (var appSymp in appointment.AppointmentSymptoms)
        {
            var symp = await symptomRepository.FindByIdAsync(appSymp.SymptomId);
            if (symp != null)
                symptoms.Add(symp);
        }

        var response = new Responses.AppointmentGetByIdResponse(
            appointment.Id,
            appointment.Date.ToString("dd-MM-yyyy"),
            appointment.Time,
            appointment.Address,
            appointment.Latitude,
            appointment.Longitude,
            appointment.Description,
            appointment.Note,
            appointment.Price,
            appointment.Status,
            mapper.Map<UserResponse>(appointment.Customer),
            mapper.Map<UserResponse>(appointment.Employee),
            mapper.Map<List<SymptomResponse>>(symptoms)
            );

        return Result.Success(response);
    }
}
