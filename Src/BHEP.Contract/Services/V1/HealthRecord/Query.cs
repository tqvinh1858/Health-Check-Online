using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.HealthRecord;
public static class Query
{
    public record GetHealthRecordByUserIdQuery(
        int UserId,
        int? DeviceId,
        string? CreatedDate) : IQuery<Responses.HealthRecordResponse>;
}
