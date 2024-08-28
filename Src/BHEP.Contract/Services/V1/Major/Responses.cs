namespace BHEP.Contract.Services.V1.Major;
public static class Responses
{
    public record MajorResponse(
       int Id,
       string Name,
       string Description);
}
