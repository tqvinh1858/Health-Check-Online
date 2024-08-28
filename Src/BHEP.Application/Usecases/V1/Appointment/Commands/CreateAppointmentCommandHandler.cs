using System.Globalization;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Persistence;
namespace BHEP.Application.Usecases.V1.Appointment.Commands;
public sealed class CreateAppointmentCommandHandler : ICommandHandler<Command.CreateAppointmentCommand, Responses.AppointmentResponse>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IAppointmentSymptomRepository AppointmentSymptomRepository;
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository AppointmentRepository,
        ApplicationDbContext context,
        IMapper mapper,
        IAppointmentSymptomRepository appointmentSymptomRepository,
        IUserRepository userRepository)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.context = context;
        this.mapper = mapper;
        AppointmentSymptomRepository = appointmentSymptomRepository;
        this.userRepository = userRepository;
    }

    public async Task<Result<Responses.AppointmentResponse>> Handle(Command.CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        DateTime.TryParseExact(request.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime scheduleDate);


        if (!await userRepository.IsExist(request.CustomerId))
            return Result.Failure<Responses.AppointmentResponse>("Customer not found");

        if (!await userRepository.IsExist(request.EmployeeId))
            return Result.Failure<Responses.AppointmentResponse>("Employee not found");

        var Appointment = new Domain.Entities.AppointmentEntities.Appointment
        {
            CustomerId = request.CustomerId,
            EmployeeId = request.EmployeeId,
            Date = scheduleDate,
            Time = request.Time,
            Price = request.Price,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            Note = request.Note,
        };
        var employee = await userRepository.FindByIdAsync(request.EmployeeId);
        if (employee.RoleId != (int)UserRole.Employee)
            return Result.Failure<Responses.AppointmentResponse>("Incorrect EmployeeId");
        try
        {
            // Check Balance Customer
            var customer = await userRepository.FindByIdAsync(request.CustomerId);
            if (request.Price > customer.Balance)
                return Result.Failure<Responses.AppointmentResponse>("Not Enough Coin In Balance");
            customer.Balance -= request.Price;
            userRepository.Update(customer);

            // Add Appointment
            AppointmentRepository.Add(Appointment);
            await context.SaveChangesAsync();

            // Add AppointmentSymptom
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
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
