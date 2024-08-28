using System.ComponentModel.DataAnnotations;
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.UserRate;
public static class Command
{
    public record CreateUserRateCommand(
        int CustomerId,
        int EmployeeId,
        int AppointmentId,
        string? Reason,
        [Range(0,5)]
        float Rate) : ICommand<Responses.UserRateResponse>;

    public record UpdateUserRateCommand(
        int? Id,
        string? Reason,
        [Range(0,5)]
        float Rate) : ICommand<Responses.UserRateResponse>;

    public record DeleteUserRateCommand(int Id) : ICommand;
}
