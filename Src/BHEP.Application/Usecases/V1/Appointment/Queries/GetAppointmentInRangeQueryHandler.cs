using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Appointment.Queries;
public sealed class GetAppointmentInRangeQueryHandler : IQueryHandler<Query.GetAppointmentInRangeQuery, PagedResult<Responses.AppointmentGetAllResponse>>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IMapper mapper;

    public GetAppointmentInRangeQueryHandler(
        IAppointmentRepository AppointmentRepository,
        IMapper mapper)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.AppointmentGetAllResponse>>> Handle(Query.GetAppointmentInRangeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Expression<Func<Domain.Entities.AppointmentEntities.Appointment, object>> includeProperties = x => x.Customer;
            var EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
                ? AppointmentRepository.FindAll(
                        x => x.Customer.RoleId == 2 && x.Customer.IsDeleted == false,
                        includeProperties: includeProperties)
                : AppointmentRepository.FindAll(
                        x => x.Customer.RoleId == 2 && x.Customer.IsDeleted == false && x.Address.Contains(request.searchTerm),
                        includeProperties: includeProperties);

            // Get Func<TEntity,TResponse> column
            var keySelector = GetSortProperty(request);

            // Asc Or Des
            EventsQuery = request.sortOrder == SortOrder.Descending
                ? EventsQuery.OrderByDescending(keySelector)
                : EventsQuery.OrderBy(keySelector);

            // GetList by Pagination
            var Events = await PagedResult<Domain.Entities.AppointmentEntities.Appointment>.CreateAsync(EventsQuery,
                request.pageIndex,
                request.pageSize);

            var itemsToRemove = new List<Domain.Entities.AppointmentEntities.Appointment>();
            foreach (var item in Events.items)
            {
                var isInRange = AppointmentRepository.IsInRange(
                                request.range,
                                double.Parse(request.latitude),
                                double.Parse(request.longitude),
                                double.Parse(item.Latitude),
                                double.Parse(item.Longitude));
                if (!isInRange)
                    itemsToRemove.Add(item);
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                Events.items.Remove(itemToRemove);
            }

            var result = mapper.Map<PagedResult<Responses.AppointmentGetAllResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static Expression<Func<Domain.Entities.AppointmentEntities.Appointment, object>> GetSortProperty(Query.GetAppointmentInRangeQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "date" => e => e.Date,
        _ => e => e.Date
    };
}

