
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Appointment;
public class Command
{
    public record CreateAppointmentCommand(
        int CustomerId,
        int EmployeeId,
        string Date,
        string Time,
        decimal Price,
        string? Address,
        string? Latitude,
        string? Longitude,
        string? Description,
        string? Note,
        List<int> Symptoms) : ICommand<Responses.AppointmentResponse>;

    public record UpdateAppointmentCommand(
        int? Id,
        string Date,
        string Time,
        decimal Price,
        string? Address,
        string? Latitude,
        string? Longitude,
        string? Description,
        string? Note,
        AppointmentStatus Status,
        List<int> Symptoms) : ICommand<Responses.AppointmentResponse>;

    public record UpdateAppointmentStatusCommand(
        int? Id,
        int? CustomerId,
        int? EmployeeId,
        AppointmentStatus Status) : ICommand<Responses.AppointmentResponse>;

    public record DeleteAppointmentCommand(int Id) : ICommand;
}
