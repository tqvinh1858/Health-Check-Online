using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using static BHEP.Contract.Services.V1.Major.Responses;

namespace BHEP.Application.Usecases.V1.User.Queries;
public sealed class GetUserIdQueryHandler : IQueryHandler<Query.GetUserByIdQuery, Responses.UserGetByIdResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IUserRateRepository userRateRepository;
    private readonly IWorkProfileRepository workProfileRepository;
    private readonly IAppointmentRepository appointmentRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IMapper mapper;

    public GetUserIdQueryHandler(
        IUserRepository userRepository,
        IMapper mapper,
        IUserRateRepository userRateRepository,
        IWorkProfileRepository workProfileRepository,
        IAppointmentRepository appointmentRepository,
        ICoinTransactionRepository coinTransactionRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.userRateRepository = userRateRepository;
        this.workProfileRepository = workProfileRepository;
        this.appointmentRepository = appointmentRepository;
        this.coinTransactionRepository = coinTransactionRepository;
    }

    public async Task<Result<Responses.UserGetByIdResponse>> Handle(Query.GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Check UserExist
        Expression<Func<Domain.Entities.UserEntities.User, object>> includeProperty = x => x.Role;
        var userExist = await userRepository.FindSingleAsync(
            predicate: x => x.IsDeleted == false && x.Id == request.Id,
            cancellationToken,
            includeProperty)
            ?? throw new UserException.UserNotFoundException();

        // getListRating
        List<Domain.Entities.UserEntities.UserRate> listRating = new List<Domain.Entities.UserEntities.UserRate>();
        if (userExist.RoleId == (int)UserRole.Employee)
            listRating = await userRateRepository.FindAll(x => x.EmployeeId == userExist.Id).ToListAsync();

        // getAvgRating
        var avgRate = await userRateRepository.GetAvgRating(userExist.Id);

        Contract.Services.V1.WorkProfile.Responses.WorkProfileResponse workProfileResponse = new Contract.Services.V1.WorkProfile.Responses.WorkProfileResponse();

        if (userExist.RoleId == (int)UserRole.Employee)
        {
            var workProfile = await workProfileRepository.FindByUserIdAsync(
                userExist.Id,
                cancellationToken,
                includeProperties: new Expression<Func<Domain.Entities.UserEntities.WorkProfile, object>>[] { x => x.Major, x => x.User })
                ?? throw new WorkProfileException.WorkProfileBadRequestException("Employee do not have WorkProfile");

            workProfileResponse.Id = workProfile.Id;
            workProfileResponse.UserId = workProfile.UserId;
            workProfileResponse.MajorId = workProfile.MajorId;
            workProfileResponse.SpecialistId = workProfile.User.SpecialistId.Value;
            workProfileResponse.FullName = workProfile.User.FullName;
            workProfileResponse.Avatar = workProfile.User.Avatar ?? "";
            workProfileResponse.Description = workProfile.User.Description ?? "";
            workProfileResponse.WorkPlace = workProfile.WorkPlace;
            workProfileResponse.Certificate = workProfile.Certificate;
            workProfileResponse.ExperienceYear = workProfile.ExperienceYear;
            workProfileResponse.AppointmentDone = appointmentRepository.CompletedByEmployeeId(workProfile.UserId);
            workProfileResponse.Price = workProfile.Price;
            workProfileResponse.Major = new MajorResponse(workProfile.Major.Id, workProfile.Major.Name, workProfile.Major.Description);
        }

        // GetTransactions
        var transactions = await coinTransactionRepository.FindAll(x => x.UserId == request.Id)
                                                            .ToListAsync();

        // GetCode
        var FamilyCodes = userExist.UserCodes
            .Select(x => x.Code.Name)
            .ToList();
        var DeviceCodes = userExist.CoinTransactions
            .Where(x => x.Devices.Any())
            .SelectMany(x => x.Devices.Select(x => new Responses.DeviceResponse(x.Id, x.Code)))
            .ToList();

        var response = new Responses.UserGetByIdResponse(
            userExist.Id,
            userExist.GeoLocationId,
            userExist.Role.Name,
            userExist.FullName,
            userExist.Email,
            userExist.PhoneNumber,
            userExist.Gender,
            userExist.Description,
            userExist.Avatar,
            userExist.Balance,
            userExist.IsActive,
            avgRate,
            workProfileResponse,
            mapper.Map<ICollection<Contract.Services.V1.Appointment.Responses.AppointmentResponse>>(userExist.AppointmentCustomers),
            mapper.Map<ICollection<Contract.Services.V1.Appointment.Responses.AppointmentResponse>>(userExist.AppointmentEmployees),
            mapper.Map<List<Contract.Services.V1.UserRate.Responses.UserRateResponse>>(listRating),
            FamilyCodes,
            DeviceCodes
            );

        return Result.Success(response);
    }

}
