
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.WorkProfile;
public class Command
{
    public record CreateWorkProfileCommand(
        int UserId,
        int MajorId,
        int SpecialistId,
        string WorkPlace,
        string Certificate,
        int ExperienceYear,
        decimal? Price) : ICommand<Responses.WorkProfileResponse>;

    public record UpdateWorkProfileCommand(
        int? Id,
        int MajorId,
        int SpecialistId,
        string WorkPlace,
        string Certificate,
        int ExperienceYear,
        decimal Price) : ICommand<Responses.WorkProfileResponse>;

    public record DeleteWorkProfileCommand(int Id) : ICommand;
}
