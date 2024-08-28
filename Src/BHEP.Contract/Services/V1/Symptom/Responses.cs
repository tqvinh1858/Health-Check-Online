namespace BHEP.Contract.Services.V1.Symptom;
public static class Responses
{
    public record SymptomResponse(
        int Id,
        string Name,
        string Description);
}
