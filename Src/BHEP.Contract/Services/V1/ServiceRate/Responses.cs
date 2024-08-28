using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.ServiceRate;
public static class Responses
{
    public record CreateServiceRateResponse(
    int Id,
    int TransactionId,
    string? Reason,
    float Rate);

    public record ServiceRateResponse(
        int Id,
        int TransactionId,
        string? Reason,
        float Rate,
        UserResponse User);

    public record UserResponse(
        int Id,
        string FullName,
        string Avatar,
        Gender Gender);
}
