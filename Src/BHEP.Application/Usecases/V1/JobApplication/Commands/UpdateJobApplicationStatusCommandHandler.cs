using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;


namespace BHEP.Application.Usecases.V1.JobApplication.Commands;
public sealed class UpdateJobApplicationStatusCommandHandler : ICommandHandler<Command.UpdateJobApplicationStatusCommand, Responses.JobApplicationResponse>
{
    private readonly IJobApplicationRepository JobApplicationRepository;
    private readonly IUserRepository userRepository;
    private readonly IWorkProfileRepository workProfileRepository;
    private readonly IMapper mapper;

    public UpdateJobApplicationStatusCommandHandler(
        IJobApplicationRepository JobApplicationRepository,
        IMapper mapper,
        IUserRepository userRepository,
        IWorkProfileRepository workProfileRepository)
    {
        this.JobApplicationRepository = JobApplicationRepository;
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.workProfileRepository = workProfileRepository;
    }

    public async Task<Result<Responses.JobApplicationResponse>> Handle(Command.UpdateJobApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        var JobApplication = await JobApplicationRepository.FindByIdAsync(request.Id.Value, cancellationToken)
            ?? throw new JobApplicationException.JobApplicationIdNotFoundException();

        // If Status-Approve  -> UpdateUser


        try
        {
            if (request.Status == Contract.Enumerations.ApplicationStatus.Approve)
            {
                if (request.SpecialistId == null)
                    throw new JobApplicationException.JobApplicationBadRequestException("Approve require SpecialistId");
                var customer = await userRepository.FindByIdAsync(request.CustomerId.Value)
                    ?? throw new JobApplicationException.JobApplicationBadRequestException("User was not found");

                customer.RoleId = request.RoleId.Value;
                customer.SpecialistId = request.SpecialistId;
                customer.Description = request.Description;
                userRepository.Update(customer);

                var workProfile = new Domain.Entities.UserEntities.WorkProfile
                {
                    Certificate = JobApplication.Certification,
                    ExperienceYear = JobApplication.ExperienceYear,
                    MajorId = JobApplication.MajorId,
                    UserId = JobApplication.CustomerId,
                    WorkPlace = JobApplication.WorkPlace,
                    Price = 0,
                };

                workProfileRepository.Add(workProfile);
            }

            JobApplication.CancelReason = string.IsNullOrEmpty(request.CancelReason) ? null : request.CancelReason;
            JobApplication.Status = request.Status;
            JobApplicationRepository.Update(JobApplication);



            var resultResponse = mapper.Map<Responses.JobApplicationResponse>(JobApplication);
            return Result.Success(resultResponse, 202);
        }
        catch (BadRequestException be)
        {
            throw new JobApplicationException.JobApplicationBadRequestException(be.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
