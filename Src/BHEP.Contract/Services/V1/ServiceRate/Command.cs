using System.ComponentModel.DataAnnotations;
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.ServiceRate;
public static class Command
{
    public record CreateServiceRateCommand(
        int UserId,
        int ServiceId,
        int TransactionId,
        string? Reason,
        [Range(0,5)]
        float Rate) : ICommand<Responses.CreateServiceRateResponse>;

    public record UpdateServiceRateCommand(
        int? Id,
        string? Reason,
        [Range(0,5)]
        float Rate) : ICommand<Responses.ServiceRateResponse>;

    public record DeleteServiceRateCommand(int Id) : ICommand;
}
