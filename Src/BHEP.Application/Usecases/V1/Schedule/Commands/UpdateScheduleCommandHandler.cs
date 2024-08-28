using System.Globalization;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Abstractions.Repositories;


namespace BHEP.Application.Usecases.V1.Schedule.Commands;
public sealed class UpdateScheduleCommandHandler : ICommandHandler<Command.UpdateScheduleCommand, Responses.ScheduleResponse>
{
    private readonly IScheduleRepository scheduleRepository;
    private readonly IMapper mapper;

    public UpdateScheduleCommandHandler(
        IScheduleRepository scheduleRepository,
        IMapper mapper)
    {
        this.scheduleRepository = scheduleRepository;
        this.mapper = mapper;
    }


    public async Task<Result<Responses.ScheduleResponse>> Handle(Command.UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!DateTime.TryParseExact(request.Date, new[] { "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime scheduleDate))
            {
                return Result.Failure<Responses.ScheduleResponse>("Invalid date format.");
            }

            var existingSchedule = await scheduleRepository.GetScheduleByEmployeeIdAndDate(request.EmployeeId, scheduleDate.ToString("yyyy-MM-dd"));

            if (existingSchedule == null || existingSchedule.Count == 0)
            {
                if (scheduleDate.Date >= TimeZones.SoutheastAsia.Date)
                {
                    var newSchedule = new Domain.Entities.UserEntities.Schedule
                    {
                        EmployeeId = request.EmployeeId,
                        Date = scheduleDate,
                        Time = string.Join(", ", request.Time.OrderBy(t => TimeSpan.ParseExact(t.Split('-')[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture)))
                    };

                    await scheduleRepository.AddSchedulesAsync(new List<Domain.Entities.UserEntities.Schedule> { newSchedule });

                    var response = scheduleRepository.ConvertToResponse(new List<Domain.Entities.UserEntities.Schedule> { newSchedule });
                    return Result.Success(response, 201);
                }
                else
                {
                    return Result.Failure<Responses.ScheduleResponse>("No schedule found for the specified date and the date is in the past.");
                }
            }
            else
            {
                var existingScheduleForDate = existingSchedule.FirstOrDefault(s => s.Date.Date == scheduleDate.Date);
                if (existingScheduleForDate == null)
                {
                    return Result.Failure<Responses.ScheduleResponse>($"No schedule found for the specified date: {request.Date}");
                }

                UpdateTime(existingScheduleForDate, request.Time);

                existingScheduleForDate.Time = string.Join(", ", existingScheduleForDate.Time.Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .OrderBy(t => TimeSpan.ParseExact(t.Split('-')[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture)));

                await scheduleRepository.UpdateSchedulesAsync(new List<Domain.Entities.UserEntities.Schedule> { existingScheduleForDate });

                var response = scheduleRepository.ConvertToResponse(new List<Domain.Entities.UserEntities.Schedule> { existingScheduleForDate });

                return Result.Success(response, 202);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update schedule: {ex.Message}");
        }
    }

    private void UpdateTime(Domain.Entities.UserEntities.Schedule schedule, string[] updatedTimeArray)
    {
        var currentTimeList = new List<string>(schedule.Time.Split(", ", StringSplitOptions.RemoveEmptyEntries));

        foreach (var newTime in updatedTimeArray)
        {
            var trimmedNewTime = newTime.Trim();
            var newTimeParts = trimmedNewTime.Split('-', StringSplitOptions.RemoveEmptyEntries);

            if (newTimeParts.Length != 2 ||
                !TimeSpan.TryParseExact(newTimeParts[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out var newStartTime) ||
                !TimeSpan.TryParseExact(newTimeParts[1].Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out var newEndTime))
            {
                throw new ArgumentException($"Invalid time format for {newTime}");
            }

            if (newStartTime >= newEndTime)
            {
                throw new ArgumentException($"Start time {newStartTime} cannot be greater than or equal to end time {newEndTime}");
            }

            bool isOverlapHandled = false;

            foreach (var existingTime in currentTimeList.ToList())
            {
                var existingTimeParts = existingTime.Trim().Split('-', StringSplitOptions.RemoveEmptyEntries);

                if (existingTimeParts.Length != 2 ||
                    !TimeSpan.TryParseExact(existingTimeParts[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out var existingStartTime) ||
                    !TimeSpan.TryParseExact(existingTimeParts[1].Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out var existingEndTime))
                {
                    throw new ArgumentException($"Invalid existing time format for {existingTime}");
                }

                if (newStartTime >= existingEndTime || newEndTime <= existingStartTime)
                {
                    continue;
                }

                if (newStartTime < existingStartTime)
                {
                    newStartTime = existingStartTime;
                }

                if (newEndTime > existingEndTime)
                {
                    newEndTime = existingEndTime;
                }

                currentTimeList.Remove(existingTime);

                currentTimeList.Add($"{newStartTime.ToString("hh\\:mm")} - {newEndTime.ToString("hh\\:mm")}");

                isOverlapHandled = true;
            }

            if (!isOverlapHandled)
            {
                currentTimeList.Add(trimmedNewTime);
            }
        }

        schedule.Time = string.Join(", ", currentTimeList.OrderBy(t => TimeSpan.ParseExact(t.Split('-')[0].Trim(), "hh\\:mm", CultureInfo.InvariantCulture)));
    }








}


