using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.Symptom.Responses;

namespace BHEP.Contract.Services.V1.Specialist;
public static class Responses
{
    public record SpecialistResponse(
        int Id,
        string Name,
        string Description);

    public record SpecialistGetByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EmployeeResponses> Employees { get; set; }
        public ICollection<SymptomResponse> Symptoms { get; set; }
    }

    public record EmployeeResponses(
        int Id,
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender);


}
