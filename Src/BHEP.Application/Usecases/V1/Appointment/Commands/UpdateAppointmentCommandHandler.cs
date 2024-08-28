using System.Globalization;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Application.Usecases.V1.Appointment.Commands;
public sealed class UpdateAppointmentCommandHandler : ICommandHandler<Command.UpdateAppointmentCommand, Responses.AppointmentResponse>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IAppointmentSymptomRepository AppointmentSymptomRepository;
    private readonly IMapper mapper;

    public UpdateAppointmentCommandHandler(
        IAppointmentRepository AppointmentRepository,
        IMapper mapper,
        IAppointmentSymptomRepository appointmentSymptomRepository)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.mapper = mapper;
        AppointmentSymptomRepository = appointmentSymptomRepository;
    }
    public async Task<Result<Responses.AppointmentResponse>> Handle(Command.UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var Appointment = await AppointmentRepository.FindByIdAsync(request.Id.Value, cancellationToken);
        if (Appointment is null)
            return Result.Failure<Responses.AppointmentResponse>("Appointment not found");

        DateTime.TryParseExact(request.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime scheduleDate);

        try
        {
            Appointment.Update(
                scheduleDate,
                request.Price,
                request.Address,
                request.Latitude,
                request.Longitude,
                request.Description,
                request.Note,
                request.Status);
            AppointmentRepository.Update(Appointment);


            await AppointmentSymptomRepository.Delete(request.Id.Value);

            List<AppointmentSymptom> appsymps = new List<AppointmentSymptom>();
            foreach (var symp in request.Symptoms)
            {
                appsymps.Add(new AppointmentSymptom()
                {
                    AppointmentId = Appointment.Id,
                    SymptomId = symp
                });
            }
            await AppointmentSymptomRepository.Add(appsymps);

            var resultResponse = mapper.Map<Responses.AppointmentResponse>(Appointment);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
