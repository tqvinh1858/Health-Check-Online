
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BHEP.Contract.Services.V1.JobApplication;
public class Command
{
    public record CreateJobApplicationCommand(
        int CustomerId,
        int MajorId,
        string FullName,
        string Certification,
        IFormFile Avatar,
        string WorkPlace,
        int ExperienceYear) : ICommand<Responses.JobApplicationResponse>;

    public record UpdateJobApplicationCommand(
        int? Id,
        int MajorId,
        string FullName,
        string Certification,
        IFormFile Avatar,
        string WorkPlace,
        int ExperienceYear) : ICommand<Responses.JobApplicationResponse>;

    public record UpdateJobApplicationStatusCommand(
        int? Id,
        int? CustomerId,
        int? SpecialistId,
        int? RoleId,
        string? Description,
        ApplicationStatus Status,
        string? CancelReason) : ICommand<Responses.JobApplicationResponse>;

    public record DeleteJobApplicationCommand(int Id) : ICommand;
}
