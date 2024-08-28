using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.Appointment.Responses;
using static BHEP.Contract.Services.V1.UserRate.Responses;
using static BHEP.Contract.Services.V1.WorkProfile.Responses;

namespace BHEP.Contract.Services.V1.User;

public static class Responses
{
    public record UserResponse(
        int Id,
        int RoleId,
        int GeoLocationId,
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string Description,
        string Avatar,
        decimal Balance,
        bool IsActive);


    public record UserDeletionRequestResponse(
       int Id,
       string FullName,
       string Email,
       string PhoneNumber,
       Gender Gender,
       string Description,
       string Avatar);



    public record UserGetAllResponse(
        int Id,
        int RoleId,
        int GeoLocationId,
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string Description,
        string Avatar,
        decimal Balance,
        bool IsActive);

    public class UserGetAllCacheResponse
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int GeoLocationId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public UserGetAllCacheResponse(
            int id,
            int roleId,
            int geoLocationId,
            string fullName,
            string email,
            string phoneNumber,
            Gender gender,
            string description,
            string avatar,
            decimal balance,
            bool isActive)
        {
            Id = id;
            RoleId = roleId;
            GeoLocationId = geoLocationId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Description = description;
            Avatar = avatar;
            Balance = balance;
            IsActive = isActive;
        }
    }

    public record DoctorResponse(
        int Id,
        int RoleId,
        int GeoLocationId,
        int? SpecialistId,
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string Description,
        string Avatar,
        decimal Balance,
        bool IsActive,
        float Rate,
        WorkProfileDoctorResponse WorkProfile
       );




    public record UserGetByIdResponse(
        int Id,
        int GeoLocationId,
        string RoleName,
        string FullName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string Description,
        string Avatar,
        decimal Balance,
        bool IsActive,
        float Rate,
        WorkProfileResponse? WorkProfile,
        ICollection<AppointmentResponse> Appointments,
        ICollection<AppointmentResponse> AppointmentsReceived,
        ICollection<UserRateResponse> Rates,
        List<string>? FamilyCodes,
        List<DeviceResponse>? DeviceCodes);

    public record DeviceResponse(
        int Id,
        string Code);

    public record CoinTransactionResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsMinus { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CreatedDate { get; set; }
    }
}
