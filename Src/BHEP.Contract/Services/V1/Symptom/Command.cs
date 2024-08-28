using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Symptom;
public static class Command
{
    public record CreateSymptomCommand(
        int SpecialistId,
        string Name,
        string Description) : ICommand<Responses.SymptomResponse>;

    public record UpdateSymptomCommand(
        int? Id,
        string Name,
        string Description) : ICommand<Responses.SymptomResponse>;

    public record DeleteSymptomCommand(int Id) : ICommand;
}
