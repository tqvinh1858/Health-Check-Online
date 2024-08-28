namespace BHEP.Contract.Services.V1.Topic;

public static class Responses
{
    public record TopicResponse(
        int Id,
        string Name,
        string Description);
}
