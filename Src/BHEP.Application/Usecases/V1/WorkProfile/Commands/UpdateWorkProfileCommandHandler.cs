using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.WorkProfile;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using static BHEP.Contract.Services.V1.Major.Responses;

namespace BHEP.Application.Usecases.V1.WorkProfile.Commands;
public sealed class UpdateWorkProfileCommandHandler : ICommandHandler<Command.UpdateWorkProfileCommand, Responses.WorkProfileResponse>
{
    private readonly IWorkProfileRepository WorkProfileRepository;
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IMajorRepository majorRepository;

    public UpdateWorkProfileCommandHandler(
        IWorkProfileRepository WorkProfileRepository,
        IAppointmentRepository appointmentRepository,
        IMajorRepository majorRepository)
    {
        this.WorkProfileRepository = WorkProfileRepository;
        AppointmentRepository = appointmentRepository;
        this.majorRepository = majorRepository;
    }
    public async Task<Result<Responses.WorkProfileResponse>> Handle(Command.UpdateWorkProfileCommand request, CancellationToken cancellationToken)
    {
        var WorkProfileExist = await WorkProfileRepository.FindByIdAsync(request.Id.Value, includeProperties: x => x.User)
            ?? throw new WorkProfileException.WorkProfileIdNotFoundException();

        var major = await majorRepository.FindByIdAsync(request.MajorId)
               ?? throw new WorkProfileException.WorkProfileBadRequestException("Major not found!");

        try
        {
            WorkProfileExist.Update(
                request.WorkPlace,
                request.MajorId,
                request.Certificate,
                request.ExperienceYear,
                request.Price);

            WorkProfileRepository.Update(WorkProfileExist);

            var specialistId = WorkProfileExist.User.SpecialistId ?? 0;

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

            return Result.Success(response, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
