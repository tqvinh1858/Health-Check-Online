using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.User.Responses;

namespace BHEP.Contract.Services.V1.JobApplication;

public static class Responses
{
    public record JobApplicationResponse(
        int Id,
        int CustomerId,
        int MajorId,
        string FullName,
        string Certification,
        string WorkPlace,
        int ExperienceYear,
        ApplicationStatus Status,
        string CancelReason);


    public record JobApplicationGetByIdResponse(
        int Id,
        int CustomerId,
        int MajorId,
        string FullName,
        string Certification,
        string WorkPlace,
        int ExperienceYear,
        ApplicationStatus Status,
        string CancelReason,
        UserResponse Customer);
}
