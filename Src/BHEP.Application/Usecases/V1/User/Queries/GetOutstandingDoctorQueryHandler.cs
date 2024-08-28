using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using static BHEP.Contract.Services.V1.Major.Responses;
using static BHEP.Contract.Services.V1.WorkProfile.Responses;

namespace BHEP.Application.Usecases.V1.User.Queries;
public sealed class GetOutstandingDoctorQueryHandler : IQueryHandler<Query.GetOutstandingDoctorQuery, PagedResult<Responses.DoctorResponse>>
{
    private readonly IUserRepository userRepository;
    private readonly IUserRateRepository userRateRepository;

    public GetOutstandingDoctorQueryHandler(
        IUserRepository userRepository,
        IUserRateRepository userRateRepository)
    {
        this.userRepository = userRepository;
        this.userRateRepository = userRateRepository;
    }

    public async Task<Result<PagedResult<Responses.DoctorResponse>>> Handle(Query.GetOutstandingDoctorQuery request, CancellationToken cancellationToken)
    {
        var doctors = await userRepository
                .FindAll(x => x.RoleId == (int)UserRole.Employee && x.IsDeleted == false)
                .ToListAsync(cancellationToken);

        var doctorRatings = await Task.WhenAll(doctors.Select(async x => new
        {
            User = x,
            Rating = await userRateRepository.GetAvgRating(x.Id)
        }));

        var orderedDoctors = doctorRatings
            .OrderByDescending(dr => dr.Rating)
            .Select(dr => dr.User)
            .ToList();

        var pagedDoctors = orderedDoctors
            .Skip((request.pageIndex - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToList();

        List<Responses.DoctorResponse> doctorResponsesList = new List<Responses.DoctorResponse>();
        foreach (var item in pagedDoctors)
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
                await userRateRepository.GetAvgRating(item.Id),
                workProfile
            );
            doctorResponsesList.Add(doctor);
        }


        var result = PagedResult<Responses.DoctorResponse>.Create(
             doctorResponsesList,
             request.pageIndex,
             request.pageSize,
             orderedDoctors.Count()
     );

        return Result.Success(result);
    }
}
