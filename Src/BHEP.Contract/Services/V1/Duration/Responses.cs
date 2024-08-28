using static BHEP.Contract.Services.V1.Service.Responses;

namespace BHEP.Contract.Services.V1.Duration;

public static class Responses
{
    public record DurationResponse(
        int Id,
        string StartDate,
        string EndDate,
        bool IsExpired,
        ServiceResponse Service);
}
