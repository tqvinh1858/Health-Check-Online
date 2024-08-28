using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Appointment.Queries;
public sealed class GetAppointmentQueryHandler : IQueryHandler<Query.GetAppointmentQuery, PagedResult<Responses.AppointmentGetAllResponse>>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IMapper mapper;

    public GetAppointmentQueryHandler(
        IAppointmentRepository AppointmentRepository,
        IMapper mapper)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.AppointmentGetAllResponse>>> Handle(Query.GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //Include
            Expression<Func<Domain.Entities.AppointmentEntities.Appointment, object>>[] includeProperties = [x => x.Employee, x => x.Customer];
            // Check value search is nullOrWhiteSpace?
            var EventsQuery = string.IsNullOrEmpty(request.SearchTerm)
                ? AppointmentRepository.FindAll(predicate: x => x.IsDeleted == false,
                                                includeProperties: includeProperties)
                : AppointmentRepository.FindAll(predicate: x => x.Address.Contains(request.SearchTerm) && x.IsDeleted == false,
                                                includeProperties: includeProperties);

            // Filter ByStatus
            EventsQuery = (request.Status == null) ? EventsQuery : EventsQuery.Where(x => x.Status == request.Status);

            // Filter by UserId
            if (request.UserId.HasValue)
            {
                EventsQuery = EventsQuery.Where(x => x.CustomerId == request.UserId || x.EmployeeId == request.UserId);
            }

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.SortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.AppointmentEntities.Appointment>.CreateAsync(EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.AppointmentGetAllResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.AppointmentEntities.Appointment, object>> GetSortProperty(Query.GetAppointmentQuery request)
    => request.SortColumn?.ToLower() switch
    {
        "date" => e => e.Date,
        _ => e => e.Date
    };
}
