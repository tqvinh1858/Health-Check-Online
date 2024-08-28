using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.UserRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.UserRate.Commands;
public sealed class CreatePostLikeCommandHandler : ICommandHandler<Command.CreateUserRateCommand, Responses.UserRateResponse>
{
    private readonly IUserRateRepository UserRateRepository;
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreatePostLikeCommandHandler(
        IUserRateRepository UserRateRepository,
        IAppointmentRepository AppointmentRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.UserRateRepository = UserRateRepository;
        this.AppointmentRepository = AppointmentRepository;
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.UserRateResponse>> Handle(Command.CreateUserRateCommand request, CancellationToken cancellationToken)
    {
        if (!await AppointmentRepository.IsExistAppointment(request.CustomerId, request.EmployeeId, request.AppointmentId))
            throw new UserRateException.UserRateBadRequestException("Appointment Is Not Exist!");

        if (await UserRateRepository.IsExistUserRate(request.CustomerId, request.AppointmentId, request.EmployeeId))
            throw new UserRateException.UserRateBadRequestException("Has been Rating!");


        try
        {
            var userRate = new Domain.Entities.UserEntities.UserRate
            {
                CustomerId = request.CustomerId,
                EmployeeId = request.EmployeeId,
                AppointmentId = request.AppointmentId,
                Reason = request.Reason,
                Rate = request.Rate,
            };

            UserRateRepository.Add(userRate);
            await context.SaveChangesAsync();

            var response = mapper.Map<Responses.UserRateResponse>(userRate);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
