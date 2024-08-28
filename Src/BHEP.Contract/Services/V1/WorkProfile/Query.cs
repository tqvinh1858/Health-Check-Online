using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.WorkProfile;
public static class Query
{
    public record GetWorkProfileByUserIdQuery(int DoctorId) : IQuery<Responses.WorkProfileResponse>;
}
