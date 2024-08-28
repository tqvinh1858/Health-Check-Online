

using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.JobApplication.Commands;
public sealed class DeleteJobApplicationCommandHandler : ICommandHandler<Command.DeleteJobApplicationCommand>
{
    private readonly IJobApplicationRepository JobApplicationRepository;
    public DeleteJobApplicationCommandHandler(IJobApplicationRepository JobApplicationRepository)
    {
        this.JobApplicationRepository = JobApplicationRepository;
    }

    public async Task<Result> Handle(Command.DeleteJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var JobApplicationExist = await JobApplicationRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new JobApplicationException.JobApplicationIdNotFoundException();

        try
        {
            JobApplicationRepository.Remove(JobApplicationExist);
            // Delete oldImage In BlobStorage
            //if (string.IsNullOrEmpty(oldImageUrl))
            //    blobStorageRepository.DeleteImageFromBlobStorage(oldImageUrl);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
