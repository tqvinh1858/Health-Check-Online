using System.Globalization;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using static BHEP.Contract.Services.V1.Schedule.Responses;

namespace BHEP.Persistence.Repositories.UserRepo;
public class ScheduleRepository : RepositoryBase<Schedule, int>, IScheduleRepository
{
    private readonly ApplicationDbContext context;
    public ScheduleRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<Schedule>?> GetScheduleByEmployeeId(int employeeId)
    {
        var listSchedule = await context.Schedule.AsNoTracking().Where(x => x.EmployeeId == employeeId).ToListAsync();
        return listSchedule;
    }

    public async Task<IReadOnlyCollection<Schedule>?> GetScheduleByDate(string date)
    {
        if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            throw new ArgumentException("Invalid date format.");
        }
        var listSchedule = await context.Schedule.AsNoTracking().Where(x => x.Date.Date == parsedDate.Date).ToListAsync();

        return listSchedule;
    }



    public async Task<IReadOnlyCollection<Schedule>?> GetScheduleByEmployeeIdAndDate(int employeeId, string date)
    {
        if (!DateTime.TryParse(date, out DateTime parsedDate))
        {
            throw new ArgumentException("Invalid date format.");
        }
        var listSchedule = await context.Schedule.AsNoTracking().Where(s => s.EmployeeId == employeeId && s.Date.Date == parsedDate.Date).ToListAsync();
        return listSchedule;
    }


    public Responses.ScheduleResponse ConvertToResponse(IReadOnlyCollection<Schedule> schedules)
    {
        var weekSchedules = schedules.Select(schedule =>
        new WeekSchedule(
            Id: schedule.Id,
            Date: schedule.Date.ToString("dd-MM-yyyy"),
            Time: schedule.Time.Split(", ", StringSplitOptions.RemoveEmptyEntries)
        )).ToList();

        return new Responses.ScheduleResponse(
            EmployeeId: schedules.First().EmployeeId,
            WeekSchedule: weekSchedules
        );
    }

    public async Task AddSchedulesAsync(List<Schedule> schedules)
    {
        AddMultiple(schedules);
        await context.SaveChangesAsync();
    }


    public async Task UpdateSchedulesAsync(List<Schedule> schedules)
    {
        UpdateMultiple(schedules);
        await context.SaveChangesAsync();
    }


}
