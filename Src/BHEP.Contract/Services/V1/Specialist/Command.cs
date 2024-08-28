using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Specialist;
public static class Command
{
    public record CreateSpecialistCommand(
        string Name,
        string Description) : ICommand<Responses.SpecialistResponse>;

    public record UpdateSpecialistCommand(
        int? Id,
        string Name,
        string Description) : ICommand<Responses.SpecialistResponse>;

    public record DeleteSpecialistCommand(int Id) : ICommand;
}
