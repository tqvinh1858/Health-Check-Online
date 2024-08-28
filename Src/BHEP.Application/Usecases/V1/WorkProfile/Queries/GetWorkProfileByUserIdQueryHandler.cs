using System.Linq.Expressions;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.WorkProfile;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using static BHEP.Contract.Services.V1.Major.Responses;

namespace BHEP.Application.Usecases.V1.WorkProfile.Queries;
public sealed class GetWorkProfileByUserIdQueryHandler : IQueryHandler<Query.GetWorkProfileByUserIdQuery, Responses.WorkProfileResponse>
{
    private readonly IWorkProfileRepository WorkProfileRepository;
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IMajorRepository majorRepository;

    public GetWorkProfileByUserIdQueryHandler(
        IWorkProfileRepository WorkProfileRepository,
        IAppointmentRepository appointmentRepository,
        IMajorRepository majorRepository)
    {
        this.WorkProfileRepository = WorkProfileRepository;
        AppointmentRepository = appointmentRepository;
        this.majorRepository = majorRepository;
    }

    public async Task<Result<Responses.WorkProfileResponse>> Handle(Query.GetWorkProfileByUserIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.UserEntities.WorkProfile, object>> includeProperty = x => x.User;
        var WorkProfileExist = await WorkProfileRepository.FindByUserIdAsync(request.DoctorId, cancellationToken, includeProperty)
            ?? throw new WorkProfileException.WorkProfileIdNotFoundException();

        var specialistId = WorkProfileExist.User.SpecialistId ?? 0;

        var major = await majorRepository.FindByIdAsync(WorkProfileExist.MajorId)
             ?? throw new WorkProfileException.WorkProfileBadRequestException("Major not found!");


        var response = new Responses.WorkProfileResponse(
            WorkProfileExist.Id,
            WorkProfileExist.UserId,
            WorkProfileExist.MajorId,
            specialistId,
            WorkProfileExist.User.FullName,
            WorkProfileExist.User.Avatar ?? "",
            WorkProfileExist.User.Description ?? "",
            WorkProfileExist.WorkPlace,
            WorkProfileExist.Certificate,
            WorkProfileExist.ExperienceYear,
            AppointmentRepository.CompletedByEmployeeId(WorkProfileExist.UserId),
            WorkProfileExist.Price,
            new MajorResponse(major.Id, major.Name, major.Description)
            );

        return Result.Success(response);
    }

}
