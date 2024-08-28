
using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.ProductRate;
public static class Responses
{
    public record CreateProductResponse(
        int Id,
        int TransactionId,
        string? Reason,
        float Rate);

    public record ProductRateResponse(
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
