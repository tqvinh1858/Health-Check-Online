using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using static BHEP.Contract.Services.V1.Major.Responses;
using static BHEP.Contract.Services.V1.WorkProfile.Responses;

namespace BHEP.Application.Usecases.V1.Doctor.Queries;
public sealed class GetDoctorQueryHandler : IQueryHandler<Query.GetDoctorQuery, PagedResult<Responses.DoctorResponse>>
{
    private readonly IUserRepository UserRepository;
    private readonly IUserRateRepository UserRateRepository;
    private readonly IMapper mapper;
    public GetDoctorQueryHandler(
        IUserRepository UserRepository,
        IUserRateRepository UserRateRepository,
        IMapper mapper)
    {
        this.UserRepository = UserRepository;
        this.UserRateRepository = UserRateRepository;
        this.mapper = mapper;
    }
    public async Task<Result<PagedResult<Responses.DoctorResponse>>> Handle(Query.GetDoctorQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.UserEntities.User> EventsQuery;

        EventsQuery = string.IsNullOrWhiteSpace(request.searchTerm)
            ? UserRepository.FindAll(x => x.IsDeleted == false)
            : UserRepository.FindAll(x => x.FullName.Contains(request.searchTerm) && x.IsDeleted == false);

        EventsQuery = EventsQuery.Where(x => x.RoleId == (int)UserRole.Employee);

        EventsQuery = request.SpecialistId is null ? EventsQuery : EventsQuery.Where(x => x.SpecialistId == request.SpecialistId);

        var keySelector = GetSortProperty(request);

        EventsQuery = request.sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.UserEntities.User>.CreateAsync(EventsQuery,
            request.pageIndex,
            request.pageSize);

        List<Responses.DoctorResponse> doctorResponsesList = new List<Responses.DoctorResponse>();

        foreach (var item in Events.items)
        {
            int appointmentDone = item.AppointmentEmployees.Count(a => a.Status == AppointmentStatus.Completed);

            var workProfile = item.Profile != null
             ? new WorkProfileDoctorResponse(
                 item.Profile.UserId,
                 item.Profile.MajorId,
                 item.Profile.WorkPlace,
                 item.Profile.Certificate,
                 item.Profile.ExperienceYear,
                 item.Profile.Price,
                 appointmentDone,
                 new MajorResponse(
                    item.Profile.Major.Id,
                    item.Profile.Major.Name,
                    item.Profile.Major.Description)
                 )
             : null;

            var doctor = new Responses.DoctorResponse
            (
                item.Id,
                item.RoleId,
                item.GeoLocationId,
                item.SpecialistId != null ? item.SpecialistId.Value : 0, item.FullName,
                item.Email,
                item.PhoneNumber,
                item.Gender,
                item.Description,
                item.Avatar,
                item.Balance,
                item.IsActive,
                await UserRateRepository.GetAvgRating(item.Id),
                workProfile
            );
            doctorResponsesList.Add(doctor);
        }

        var result = PagedResult<Responses.DoctorResponse>.Create(
            doctorResponsesList,
            Events.pageIndex,
            Events.pageSize,
            EventsQuery.Count()
        );

        return Result.Success(result);
    }

    private static Expression<Func<Domain.Entities.UserEntities.User, object>> GetSortProperty(Query.GetDoctorQuery request)
    => request.sortColumn?.ToLower() switch
    {
        "fullname" => e => e.FullName,
        "createddate" => e => e.CreatedDate,
        _ => e => e.FullName
    };
}
