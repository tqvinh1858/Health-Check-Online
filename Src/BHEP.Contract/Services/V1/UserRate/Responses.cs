namespace BHEP.Contract.Services.V1.UserRate;
public static class Responses
{
    public record UserRateResponse(
        int Id,
        int CustomerId,
        int EmployeeId,
        int AppointmentId,
        string? Reason,
        float Rate);
}
