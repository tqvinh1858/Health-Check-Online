using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Appointment.Commands;
public sealed class UpdateAppointmentStatusCommandHandler : ICommandHandler<Command.UpdateAppointmentStatusCommand, Responses.AppointmentResponse>
{
    private readonly IAppointmentRepository appointmentRepository;
    private readonly IUserRepository userRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IMapper mapper;
    private const string TitleCancel = "Hủy đặt lịch";
    private const string TitleRefused = "Lịch đặt khám bị từ chối";
    private const string TitleCompleted = "Hoàn thành ca đặt khám";
    public UpdateAppointmentStatusCommandHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper,
        IUserRepository userRepository,
        ICoinTransactionRepository coinTransactionRepository)
    {
        this.appointmentRepository = appointmentRepository;
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.coinTransactionRepository = coinTransactionRepository;
    }

    public async Task<Result<Responses.AppointmentResponse>> Handle(Command.UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(
            id: request.Id.Value,
            cancellationToken: cancellationToken,
            includeProperties: [x => x.Employee, x => x.Customer]);
        if (appointment is null)
            return Result.Failure<Responses.AppointmentResponse>("Appointment not found");

        if (appointment.Status != AppointmentStatus.Waiting && appointment.Status != AppointmentStatus.Received)
            return Result.Failure<Responses.AppointmentResponse>("Appointment have been Handle");

        try
        {
            // Handle Balance
            switch (request.Status)
            {
                case AppointmentStatus.Refused:
                    await UpdateBalanceCustomer(request.CustomerId.Value, appointment, TitleRefused);
                    break;
                case AppointmentStatus.Completed:
                    await UpdateBalanceEmployee(request.EmployeeId.Value, appointment, TitleCompleted);
                    break;
                case AppointmentStatus.Cancel:
                    if (appointment.Status == AppointmentStatus.Received)
                        throw new AppointmentException.AppointmentBadRequestException("Appointment have been Received");
                    else
                        await UpdateBalanceCustomer(request.CustomerId.Value, appointment, TitleCancel);
                    break;
                default:
                    break;
            }

            // Update Status
            appointment.Status = request.Status;
            appointmentRepository.Update(appointment);

            var resultResponse = mapper.Map<Responses.AppointmentResponse>(appointment);
            return Result.Success(resultResponse, 202);
        }
        catch (NotFoundException ne)
        {
            return Result.Failure<Responses.AppointmentResponse>(ne.Message);
        }
        catch (BadRequestException be)
        {
            return Result.Failure<Responses.AppointmentResponse>(be.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task UpdateBalanceEmployee(int employeeId, Domain.Entities.AppointmentEntities.Appointment appointment, string title)
    {
        // Check User
        var user = await userRepository.FindByIdAsync(employeeId)
            ?? throw new AppointmentException.UserNotFoundException("Employee Not Found");

        if (user.RoleId != (int)UserRole.Employee)
            throw new AppointmentException.AppointmentBadRequestException("User is not Employee");

        // Create CoinTransaction
        var coinTransaction = new Domain.Entities.SaleEntities.CoinTransaction
        {
            UserId = user.Id,
            Amount = appointment.Price,
            IsMinus = false,
            Title = title,
            Description = appointment.Description,
            Type = CoinTransactionType.Appointment
        };

        coinTransactionRepository.Add(coinTransaction);

        // UpdateBalance
        user.Balance += appointment.Price;
        userRepository.Update(user);
    }

    private async Task UpdateBalanceCustomer(int customerId, Domain.Entities.AppointmentEntities.Appointment appointment, string title)
    {
        // Check User
        var user = await userRepository.FindByIdAsync(customerId)
            ?? throw new AppointmentException.UserNotFoundException("Customer Not Found");

        if (user.RoleId != (int)UserRole.Customer)
            throw new AppointmentException.AppointmentBadRequestException("User is not Customer");

        // Create CoinTransaction
        var coinTransaction = new Domain.Entities.SaleEntities.CoinTransaction
        {
            UserId = user.Id,
            Amount = appointment.Price,
            IsMinus = false,
            Title = title,
            Description = appointment.Description,
            Type = CoinTransactionType.Appointment
        };

        coinTransactionRepository.Add(coinTransaction);

        // UpdateBalance
        user.Balance += appointment.Price;
        userRepository.Update(user);
    }
}
