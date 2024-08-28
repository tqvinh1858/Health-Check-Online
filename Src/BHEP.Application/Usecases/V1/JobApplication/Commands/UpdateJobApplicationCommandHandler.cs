using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;


namespace BHEP.Application.Usecases.V1.JobApplication.Commands;
public sealed class UpdateJobApplicationCommandHandler : ICommandHandler<Command.UpdateJobApplicationCommand, Responses.JobApplicationResponse>
{
    private readonly IJobApplicationRepository JobApplicationRepository;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UpdateJobApplicationCommandHandler(
        IJobApplicationRepository JobApplicationRepository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        this.JobApplicationRepository = JobApplicationRepository;
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Result<Responses.JobApplicationResponse>> Handle(Command.UpdateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var JobApplication = await JobApplicationRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new JobApplicationException.JobApplicationIdNotFoundException();

        if (JobApplication.Status != Contract.Enumerations.ApplicationStatus.Processing)
            throw new JobApplicationException.JobApplicationBadRequestException("JobApplication was processed");

        try
        {
            JobApplication.Update(
                request.FullName,
                request.Certification,
                request.MajorId,
                request.WorkPlace,
                request.ExperienceYear);

            JobApplicationRepository.Update(JobApplication);

            var resultResponse = mapper.Map<Responses.JobApplicationResponse>(JobApplication);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
