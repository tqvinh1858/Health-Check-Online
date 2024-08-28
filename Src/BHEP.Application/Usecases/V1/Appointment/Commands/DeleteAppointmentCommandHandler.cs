using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;
namespace BHEP.Application.Usecases.V1.Appointment.Commands;
public sealed class DeleteAppointmentCommandHandler : ICommandHandler<Command.DeleteAppointmentCommand>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly ICoinTransactionRepository coinTransactionRepository;
    private readonly IUserRepository userRepository;
    public DeleteAppointmentCommandHandler(
        IAppointmentRepository AppointmentRepository,
        ICoinTransactionRepository coinTransactionRepository,
        IUserRepository userRepository)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.coinTransactionRepository = coinTransactionRepository;
        this.userRepository = userRepository;
    }

    public async Task<Result> Handle(Command.DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await AppointmentRepository.FindByIdAsync(request.Id, cancellationToken);
        if (appointment is null)
            return Result.Failure<Command.DeleteAppointmentCommand>("Appointment not found");

        var user = await userRepository.FindByIdAsync(appointment.CustomerId, cancellationToken);
        if (user is null)
            return Result.Failure<Command.DeleteAppointmentCommand>("Customer not found");

        try
        {
            var coinTransaction = new Domain.Entities.SaleEntities.CoinTransaction
            {
                UserId = appointment.CustomerId,
                Amount = appointment.Price,
                IsMinus = false,
                Title = "Hủy lịch đặt khám",
                Description = appointment.Description,
                Type = CoinTransactionType.Appointment
            };

            // Update

            AppointmentRepository.Remove(appointment);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
