using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.Symptom.Responses;
using static BHEP.Contract.Services.V1.User.Responses;

namespace BHEP.Contract.Services.V1.Appointment;

public static class Responses
{
    public record AppointmentResponse()
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAvatar { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAvatar { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public AppointmentStatus Status { get; set; }
    }
    public record AppointmentGetAllResponse()
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAvatar { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAvatar { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public AppointmentStatus Status { get; set; }
    }


    public record AppointmentGetByIdResponse(
        int Id,
        string Date,
        string Time,
        string Address,
        string Latitude,
        string Longitude,
        string Description,
        string Note,
        decimal Price,
        AppointmentStatus Status,
        UserResponse Customer,
        UserResponse Employee,
        List<SymptomResponse> Symptoms
        );
}
