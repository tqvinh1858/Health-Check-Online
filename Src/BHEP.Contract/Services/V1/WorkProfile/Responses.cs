using static BHEP.Contract.Services.V1.Major.Responses;

namespace BHEP.Contract.Services.V1.WorkProfile;

public static class Responses
{
    public class WorkProfileResponse
    {
        public WorkProfileResponse() { }
        public WorkProfileResponse(int id, int userId, int majorId, int specialistId, string fullName, string avatar, string description, string workPlace, string certificate, int experienceYear, int appointmentDone, decimal price, MajorResponse major)
        {
            Id = id;
            UserId = userId;
            MajorId = majorId;
            SpecialistId = specialistId;
            FullName = fullName;
            Avatar = avatar;
            Description = description;
            WorkPlace = workPlace;
            Certificate = certificate;
            ExperienceYear = experienceYear;
            AppointmentDone = appointmentDone;
            Price = price;
            Major = major;
        }

        public void Update(int id, int userId, int majorId, int specialistId, string fullName, string avatar, string description, string workPlace, string certificate, int experienceYear, int appointmentDone, decimal price, MajorResponse major)
        {
            Id = id;
            UserId = userId;
            MajorId = majorId;
            SpecialistId = specialistId;
            FullName = fullName;
            Avatar = avatar;
            Description = description;
            WorkPlace = workPlace;
            Certificate = certificate;
            ExperienceYear = experienceYear;
            AppointmentDone = appointmentDone;
            Price = price;
            Major = major;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MajorId { get; set; }
        public int SpecialistId { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string WorkPlace { get; set; }
        public string Certificate { get; set; }
        public int ExperienceYear { get; set; }
        public int AppointmentDone { get; set; }
        public decimal Price { get; set; }
        public MajorResponse Major { get; set; }
    }

    public record WorkProfileDoctorResponse(
        int UserId,
        int MajorId,
        string WorkPlace,
        string Certificate,
        int ExperienceYear,
        decimal Price,
        int AppointmentDone,
        MajorResponse Major
        );

}
