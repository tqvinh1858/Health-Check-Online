using AutoMapper;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Domain.Entities;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities.BlogEntities;
using BHEP.Domain.Entities.PostEntities;
using BHEP.Domain.Entities.SaleEntities;
using BHEP.Domain.Entities.UserEntities;
using Net.payOS.Types;


namespace BHEP.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        /*==================== V1 ====================*/

        #region AppointmentMap
        // Appointment
        CreateMap<Appointment, Contract.Services.V1.Appointment.Responses.AppointmentResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MM-yyyy")))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.CustomerAvatar, opt => opt.MapFrom(src => src.Customer.Avatar))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
            .ForMember(dest => dest.EmployeeAvatar, opt => opt.MapFrom(src => src.Employee.Avatar))
            .ReverseMap();

        CreateMap<Appointment, BHEP.Contract.Services.V1.Appointment.Responses.AppointmentGetAllResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MM-yyyy")))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.CustomerAvatar, opt => opt.MapFrom(src => src.Customer.Avatar))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
            .ForMember(dest => dest.EmployeeAvatar, opt => opt.MapFrom(src => src.Employee.Avatar))
            .ReverseMap();
        CreateMap<PagedResult<Appointment>, PagedResult<BHEP.Contract.Services.V1.Appointment.Responses.AppointmentGetAllResponse>>().ReverseMap();


        // Specialist
        CreateMap<Specialist, Contract.Services.V1.Specialist.Responses.SpecialistResponse>().ReverseMap();
        CreateMap<Specialist, Contract.Services.V1.Specialist.Responses.SpecialistGetByIdResponse>().ReverseMap();
        CreateMap<PagedResult<Specialist>, PagedResult<Contract.Services.V1.Specialist.Responses.SpecialistResponse>>().ReverseMap();
        CreateMap<User, Contract.Services.V1.Specialist.Responses.EmployeeResponses>();

        // Symptom
        CreateMap<Symptom, Contract.Services.V1.Symptom.Responses.SymptomResponse>().ReverseMap();
        #endregion

        #region BlogMap
        //Topic
        CreateMap<Topic, Contract.Services.V1.Topic.Responses.TopicResponse>().ReverseMap();
        CreateMap<PagedResult<Topic>, PagedResult<Contract.Services.V1.Topic.Responses.TopicResponse>>().ReverseMap();

        // Blog
        CreateMap<BlogPhoto, Contract.Services.V1.Blog.Responses.PhotoResponse>().ReverseMap();
        CreateMap<Topic, Contract.Services.V1.Blog.Responses.TopicResponse>().ReverseMap();
        #endregion

        #region SaleMap
        // CoinTransaction
        CreateMap<Voucher, Contract.Services.V1.CoinTransaction.Responses.VoucherResponse>().ReverseMap();
        CreateMap<Service, Contract.Services.V1.CoinTransaction.Responses.ServiceResponse>().ReverseMap();
        CreateMap<Product, Contract.Services.V1.CoinTransaction.Responses.ProductResponse>().ReverseMap();
        CreateMap<Device, Contract.Services.V1.CoinTransaction.Responses.DeviceResponse>().ReverseMap();
        CreateMap<CoinTransaction, Contract.Services.V1.CoinTransaction.Responses.CoinTransactionResponse>().ReverseMap();
        CreateMap<PagedResult<CoinTransaction>, PagedResult<Contract.Services.V1.CoinTransaction.Responses.CoinTransactionResponse>>().ReverseMap();


        // Payment
        CreateMap<Payment, Contract.Services.V1.Payment.Responses.PaymentResponse>().ReverseMap()
                .ForMember(dest => dest.Method, opt => opt.Ignore()); // Ignore Method for initial mapping


        CreateMap<Payment, Responses.PayOSResponse>()
             .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));

        CreateMap<CreatePaymentResult, Contract.Services.V1.Payment.Responses.PayOSResponse>().ReverseMap();

        // Product
        CreateMap<Product, Contract.Services.V1.Product.Responses.ProductResponse>().ReverseMap();
        CreateMap<PagedResult<Product>, PagedResult<Contract.Services.V1.Product.Responses.ProductResponse>>().ReverseMap();

        // ProductRate
        CreateMap<ProductRate, Contract.Services.V1.ProductRate.Responses.ProductRateResponse>().ReverseMap();
        CreateMap<ProductRate, Contract.Services.V1.ProductRate.Responses.CreateProductResponse>().ReverseMap();
        CreateMap<PagedResult<ProductRate>, PagedResult<Contract.Services.V1.ProductRate.Responses.ProductRateResponse>>().ReverseMap();
        CreateMap<User, Contract.Services.V1.ProductRate.Responses.UserResponse>().ReverseMap();

        // Service
        CreateMap<Service, Contract.Services.V1.Service.Responses.ServiceResponse>().ReverseMap();
        CreateMap<PagedResult<Service>, PagedResult<Contract.Services.V1.Service.Responses.ServiceResponse>>().ReverseMap();

        // ServiceRate
        CreateMap<ServiceRate, Contract.Services.V1.ServiceRate.Responses.ServiceRateResponse>().ReverseMap();
        CreateMap<ServiceRate, Contract.Services.V1.ServiceRate.Responses.CreateServiceRateResponse>().ReverseMap();
        CreateMap<PagedResult<ServiceRate>, PagedResult<Contract.Services.V1.ServiceRate.Responses.ServiceRateResponse>>().ReverseMap();
        CreateMap<User, Contract.Services.V1.ServiceRate.Responses.UserResponse>().ReverseMap();

        // Device
        CreateMap<Device, Contract.Services.V1.Device.Responses.DeviceResponse>().ReverseMap();
        CreateMap<PagedResult<Device>, PagedResult<Contract.Services.V1.Device.Responses.DeviceResponse>>().ReverseMap();

        // Voucher
        CreateMap<Voucher, Contract.Services.V1.Voucher.Responses.VoucherResponse>().ReverseMap();
        CreateMap<PagedResult<Voucher>, PagedResult<Contract.Services.V1.Voucher.Responses.VoucherResponse>>().ReverseMap();

        #endregion

        #region UserMap
        // GeoLocation
        CreateMap<GeoLocation, Contract.Services.V1.GeoLocation.Responses.GeoLocationResponse>().ReverseMap();
        CreateMap<PagedResult<GeoLocation>, PagedResult<Contract.Services.V1.GeoLocation.Responses.GeoLocationResponse>>().ReverseMap();

        // JobApplication
        CreateMap<JobApplication, Contract.Services.V1.JobApplication.Responses.JobApplicationResponse>().ReverseMap();
        CreateMap<JobApplication, Contract.Services.V1.JobApplication.Responses.JobApplicationGetByIdResponse>().ReverseMap();
        CreateMap<PagedResult<JobApplication>, PagedResult<Contract.Services.V1.JobApplication.Responses.JobApplicationResponse>>().ReverseMap();

        // Role
        CreateMap<Role, Contract.Services.V1.Role.Responses.RoleResponse>().ReverseMap();
        CreateMap<PagedResult<Role>, PagedResult<Contract.Services.V1.Role.Responses.RoleResponse>>().ReverseMap();

        // Schedule
        CreateMap<Schedule, Contract.Services.V1.Schedule.Responses.ScheduleResponse>().ReverseMap();

        // User
        CreateMap<User, Contract.Services.V1.User.Responses.UserResponse>().ReverseMap();
        CreateMap<User, Contract.Services.V1.User.Responses.UserGetAllResponse>().ReverseMap();
        CreateMap<User, Contract.Services.V1.User.Responses.UserGetByIdResponse>().ReverseMap();
        CreateMap<PagedResult<User>, PagedResult<Contract.Services.V1.User.Responses.UserGetAllResponse>>().ReverseMap();
        CreateMap<CoinTransaction, Contract.Services.V1.User.Responses.CoinTransactionResponse>().ReverseMap();

        // UserRate
        CreateMap<UserRate, Contract.Services.V1.UserRate.Responses.UserRateResponse>().ReverseMap();
        CreateMap<PagedResult<UserRate>, PagedResult<Contract.Services.V1.UserRate.Responses.UserRateResponse>>().ReverseMap();


        // HealthRecord
        CreateMap<HealthRecord, Contract.Services.V1.HealthRecord.Responses.HealthRecordResponse>().ReverseMap();

        // DeletionRequest
        CreateMap<DeletionRequest, Contract.Services.V1.DeletionRequest.Responses.DeletionRequestResponse>().ReverseMap();
        CreateMap<DeletionRequest, Contract.Services.V1.DeletionRequest.Responses.DeletionRequestGetByIdResponse>().ReverseMap();
        CreateMap<PagedResult<DeletionRequest>, PagedResult<Contract.Services.V1.DeletionRequest.Responses.DeletionRequestResponse>>().ReverseMap();

        #endregion

        #region PostMap
        // Post
        CreateMap<Post, Contract.Services.V1.Post.Responses.PostResponse>().ReverseMap();
        CreateMap<PostLike, Contract.Services.V1.PostLike.Responses.PostLikeResponse>().ReverseMap();

        // Comment
        CreateMap<Comment, Contract.Services.V1.Comment.Responses.CommentResponse>().ReverseMap();

        // CommentLike
        CreateMap<CommentLike, Contract.Services.V1.CommentLike.Responses.CommentLikeResponse>();

        // Reply
        CreateMap<Reply, Contract.Services.V1.Reply.Responses.ReplyResponse>().ReverseMap();

        // ReplyLike
        CreateMap<ReplyLike, Contract.Services.V1.ReplyLike.Responses.ReplyLikeResponse>().ReverseMap();
        #endregion


        //Major
        CreateMap<Major, Contract.Services.V1.Major.Responses.MajorResponse>().ReverseMap();
        CreateMap<PagedResult<Major>, PagedResult<Contract.Services.V1.Major.Responses.MajorResponse>>().ReverseMap();


        /*==================== V2 ====================*/
        #region V2
        // Role
        CreateMap<Role, Contract.Services.V2.Role.Responses.RoleResponse>().ReverseMap();
        CreateMap<PagedResult<Role>, PagedResult<Contract.Services.V2.Role.Responses.RoleResponse>>().ReverseMap();

        #endregion
    }



}
