using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.User.Responses;

namespace BHEP.Contract.Services.V1.DeletionRequest;
public static class Responses
{
    public record DeletionRequestResponse(
        int Id,
        int UserId,
        string? Reason,
        DeletionRequestStatus Status,
        DateTime CreatedDate,
        DateTime? ProccessedDate);
    
    

    public record DeletionRequestGetByIdResponse(
        int Id,
        int UserId,
        string? Reason,
        DeletionRequestStatus Status,
        DateTime CreatedDate,
        DateTime? ProccessedDate,
        UserResponse User);
}
