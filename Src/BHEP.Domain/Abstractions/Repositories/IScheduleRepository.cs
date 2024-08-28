using BHEP.Contract.Services.V1.Schedule;
using BHEP.Domain.Entities.UserEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IScheduleRepository : IRepositoryBase<Schedule, int>
{
    Task<IReadOnlyCollection<Schedule>?> GetScheduleByEmployeeId(int employeeId);
    Task<IReadOnlyCollection<Schedule>?> GetScheduleByDate(string date);
    Task<IReadOnlyCollection<Schedule>?> GetScheduleByEmployeeIdAndDate(int employeeId, string date);
    Responses.ScheduleResponse ConvertToResponse(IReadOnlyCollection<Schedule> schedules);
    Task AddSchedulesAsync(List<Schedule> schedules);
    Task UpdateSchedulesAsync(List<Schedule> schedules);
}
