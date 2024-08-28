namespace BHEP.Contract.Services.V1.HealthRecord;

public static class Responses
{
    public record HealthRecordResponse(
        int UserId,
        List<HealthParamResponse> HealthParams);

    public record HealthParamResponse(
        int Id,
        int DeviceId,
        float Temp,
        float HeartBeat,
        float ESpO2,
        string CreatedDate);
}
