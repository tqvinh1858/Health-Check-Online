using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.DeletionRequest;
public class Command
{
    public record CreateDeletionRequestCommand(
        int UserId,
        string? Reason) : ICommand<Responses.DeletionRequestResponse>;

    public record UpdateDeletionRequestCommand(
        int Id,
        int UserId,
        string? Reason,
        DeletionRequestStatus Status,
        DateTime? ProccessedDate) : ICommand<Responses.DeletionRequestResponse>;

    public record DeleteDeletionRequestCommand(int Id) : ICommand;
}
