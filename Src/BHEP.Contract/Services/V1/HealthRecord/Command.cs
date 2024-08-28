
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.HealthRecord;
public class Command
{
    public record CreateHealthRecordCommand(
        int UserId,
        int DeviceId,
        List<HealthParam> HealthParams) : ICommand<Responses.HealthRecordResponse>;

    public record HealthParam(
        float Temp,
        float HeartBeat,
        float ESpO2);
}
