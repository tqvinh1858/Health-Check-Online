using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Major;
public class Command
{
    public record CreateMajorCommand(
        string Name,
        string Description) : ICommand<Responses.MajorResponse>;

    public record UpdateMajorCommand(
        int Id,
        string Name,
        string Description) : ICommand<Responses.MajorResponse>;

    public record DeleteMajorCommand(int Id) : ICommand;
}
