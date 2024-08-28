using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Schedule.Commands;
public sealed class DeleteScheduleCommandHandler : ICommandHandler<Command.DeleteScheduleCommand>
{
    private readonly IScheduleRepository scheduleRepository;
    public DeleteScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        this.scheduleRepository = scheduleRepository;
    }

    public async Task<Result> Handle(Command.DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var scheduleToDelete = await scheduleRepository.FindByIdAsync(request.ScheduleId, cancellationToken)
            ?? throw new ScheduleException.ScheduleNotFoundException();

        if (scheduleToDelete.EmployeeId != request.EmployeeId)
            throw new ScheduleException.EmployeeNotAuthorizedException();

        try
        {
            scheduleRepository.Remove(scheduleToDelete);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
