using System.Globalization;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;


namespace BHEP.Application.Usecases.V1.Schedule.Commands;
public sealed class CreateScheduleCommandHandler : ICommandHandler<Command.CreateScheduleCommand, Responses.ScheduleResponse>
{
    private readonly IScheduleRepository scheduleRepository;
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateScheduleCommandHandler(
        IScheduleRepository scheduleRepository,
        IUserRepository userRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.scheduleRepository = scheduleRepository;
        this.userRepository = userRepository;
        this.context = context;
        this.mapper = mapper;
    }



    public async Task<Result<Responses.ScheduleResponse>> Handle(Command.CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await userRepository.FindByIdAsync(request.EmployeeId, cancellationToken)
                                ?? throw new ScheduleException.EmployeeNotFoundException();

            var schedules = new List<Domain.Entities.UserEntities.Schedule>();

            foreach (var scheduleInfo in request.Schedules)
            {
                if (!DateTime.TryParseExact(scheduleInfo.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime scheduleDate))
                {
                    return Result.Failure<Responses.ScheduleResponse>("Invalid date format.");
                }

                if (scheduleDate.Date < DateTime.Today)
                {
                    return Result.Failure<Responses.ScheduleResponse>("Cannot schedule in the past.");
                }

                if (!IsValidTimeFormat(scheduleInfo.Time))
                {
                    return Result.Failure<Responses.ScheduleResponse>("Invalid time format.");
                }

                if (await ScheduleHasOverlap(request.EmployeeId, scheduleDate, scheduleInfo.Time))
                {
                    return Result.Failure<Responses.ScheduleResponse>("New schedule has overlapping time slots.");
                }

                if (await ScheduleExistsForDate(request.EmployeeId, scheduleDate))
                {
                    return Result.Failure<Responses.ScheduleResponse>("A schedule already exists for the specified date.");
                }

                var schedule = new Domain.Entities.UserEntities.Schedule
                {
                    EmployeeId = request.EmployeeId,
                    Date = scheduleDate,
                    Time = string.Join(", ", scheduleInfo.Time)
                };

                schedules.Add(schedule);
            }

            await scheduleRepository.AddSchedulesAsync(schedules);

            var response = scheduleRepository.ConvertToResponse(schedules);

            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<bool> ScheduleHasOverlap(int employeeId, DateTime scheduleDate, IEnumerable<string> newTimeList)
    {
        var existingSchedules = await scheduleRepository.GetScheduleByEmployeeIdAndDate(employeeId, scheduleDate.ToString("yyyy-MM-dd"));

        foreach (var existingSchedule in existingSchedules)
        {
            foreach (var existingTime in existingSchedule.Time.Split(", ", StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (var newTime in newTimeList)
                {
                    if (IsOverlapping(newTime, existingTime))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool IsOverlapping(string newTime, string existingTime)
    {
        var newTimeParts = newTime.Split('-');
        var existingTimeParts = existingTime.Split('-');

        if (newTimeParts.Length != 2 || existingTimeParts.Length != 2)
        {
            throw new ArgumentException("Invalid time format.");
        }

        var newStartTime = TimeSpan.ParseExact(newTimeParts[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture);
        var newEndTime = TimeSpan.ParseExact(newTimeParts[1].Trim(), "hh\\:mm", CultureInfo.InvariantCulture);

        var existingStartTime = TimeSpan.ParseExact(existingTimeParts[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture);
        var existingEndTime = TimeSpan.ParseExact(existingTimeParts[1].Trim(), "hh\\:mm", CultureInfo.InvariantCulture);

        return newStartTime < existingEndTime && newEndTime > existingStartTime;
    }

    private bool IsValidTimeFormat(IEnumerable<string> timeList)
    {
        foreach (var time in timeList)
        {
            var timeParts = time.Split('-');
            if (timeParts.Length != 2)
            {
                return false;
            }

            if (!TimeSpan.TryParseExact(timeParts[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out _)
                || !TimeSpan.TryParseExact(timeParts[1].Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out _))
            {
                return false;
            }
        }

        return true;
    }



    private async Task<bool> ScheduleExistsForDate(int employeeId, DateTime scheduleDate)
    {
        var existingSchedules = await scheduleRepository.GetScheduleByEmployeeIdAndDate(employeeId, scheduleDate.ToString("yyyy-MM-dd"));
        return existingSchedules.Any();
    }


}
