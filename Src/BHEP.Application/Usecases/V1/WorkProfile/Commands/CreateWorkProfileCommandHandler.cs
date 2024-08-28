using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.WorkProfile;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;
using static BHEP.Contract.Services.V1.Major.Responses;

namespace BHEP.Application.Usecases.V1.WorkProfile.Commands;
public sealed class CreateWorkProfileCommandHandler : ICommandHandler<Command.CreateWorkProfileCommand, Responses.WorkProfileResponse>
{
    private readonly IWorkProfileRepository WorkProfileRepository;
    private readonly IAppointmentRepository appointmentRepository;
    private readonly IMajorRepository majorRepository;
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;

    public CreateWorkProfileCommandHandler(
        IWorkProfileRepository WorkProfileRepository,
        IUserRepository userRepository,
        ApplicationDbContext context,
        IAppointmentRepository appointmentRepository,
        IMajorRepository majorRepository)
    {
        this.WorkProfileRepository = WorkProfileRepository;
        this.userRepository = userRepository;
        this.context = context;
        this.appointmentRepository = appointmentRepository;
        this.majorRepository = majorRepository;
    }
    public async Task<Result<Responses.WorkProfileResponse>> Handle(Command.CreateWorkProfileCommand request, CancellationToken cancellationToken)
    {
        var existProfile = await WorkProfileRepository.FindByUserIdAsync(request.UserId, includeProperties: x => x.User);
        if (existProfile != null)
            throw new WorkProfileException.WorkProfileBadRequestException("WorkProfile is existed!");

        var userExist = await userRepository.FindByIdAsync(request.UserId)
            ?? throw new WorkProfileException.UserIdNotFoundException();

        if (userExist.RoleId != (int)UserRole.Employee)
            throw new WorkProfileException.WorkProfileBadRequestException("Only Employee Have Work Profile!");

        var major = await majorRepository.FindByIdAsync(request.MajorId)
               ?? throw new WorkProfileException.WorkProfileBadRequestException("Major not found!");


        var workProfile = new Domain.Entities.UserEntities.WorkProfile
        {
            UserId = request.UserId,
            MajorId = request.MajorId,
            Certificate = request.Certificate,
            ExperienceYear = request.ExperienceYear,
            Price = request.Price ?? 0,
            WorkPlace = request.WorkPlace,
        };

        try
        {
            WorkProfileRepository.Add(workProfile);
            await context.SaveChangesAsync();

            var response = new Responses.WorkProfileResponse(
                workProfile.Id,
                workProfile.UserId,
                workProfile.MajorId,
                userExist.SpecialistId ?? 0,
                userExist.FullName,
                userExist.Avatar ?? "",
                userExist.Description ?? "",
                workProfile.WorkPlace,
                workProfile.Certificate,
                workProfile.ExperienceYear,
                0,
                workProfile.Price,
                new MajorResponse(major.Id, major.Name, major.Description)
                );
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
