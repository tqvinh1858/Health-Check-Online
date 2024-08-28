using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using BHEP.Persistence;
using Microsoft.AspNetCore.Http;


namespace BHEP.Application.Usecases.V1.JobApplication.Commands;
public sealed class CreateJobApplicationCommandHandler : ICommandHandler<Command.CreateJobApplicationCommand, Responses.JobApplicationResponse>
{
    private readonly IJobApplicationRepository JobApplicationRepository;
    private readonly ApplicationDbContext context;
    private readonly IMajorRepository majorRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageRepository;
    public CreateJobApplicationCommandHandler(
        IJobApplicationRepository JobApplicationRepository,
        ApplicationDbContext context,
        IMapper mapper,
        IBlobStorageService blobStorageRepository,
        IMajorRepository majorRepository)
    {
        this.JobApplicationRepository = JobApplicationRepository;
        this.context = context;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
        this.majorRepository = majorRepository;
    }

    public async Task<Result<Responses.JobApplicationResponse>> Handle(Command.CreateJobApplicationCommand request, CancellationToken cancellationToken)
    {
        var existJobApplication = await JobApplicationRepository.FindSingleAsync(predicate: x => x.CustomerId == request.CustomerId);
        if (existJobApplication != null)
            throw new JobApplicationException.JobApplicationBadRequestException("User can only create 1 application");

        var major = await majorRepository.FindByIdAsync(request.MajorId)
            ?? throw new JobApplicationException.JobApplicationBadRequestException("Major not found");

        var nameImage = $"{request.CustomerId}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
        var avatarUrl = await blobStorageRepository.UploadFormFormFile(request.Avatar, nameImage, "jobapplications")
            ?? throw new Exception("Upload File fail");

        var JobApplication = new Domain.Entities.UserEntities.JobApplication
        {
            CustomerId = request.CustomerId,
            FullName = request.FullName,
            Certification = request.Certification,
            MajorId = request.MajorId,
            Avatar = avatarUrl,
            WorkPlace = request.WorkPlace,
            ExperienceYear = request.ExperienceYear,
            Status = ApplicationStatus.Processing,
        };

        try
        {
            JobApplicationRepository.Add(JobApplication);
            await context.SaveChangesAsync();
            var resultResponse = mapper.Map<Responses.JobApplicationResponse>(JobApplication);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(avatarUrl))
                await blobStorageRepository.DeleteBlobSnapshotsAsync(avatarUrl);
            throw new Exception(ex.Message);
        }
    }

    public static IFormFile ConvertToIFormFile(string base64String, string fileName)
    {
        byte[] bytes = Convert.FromBase64String(base64String);

        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            MemoryStream stream = new MemoryStream(memoryStream.ToArray());
            IFormFile file = new FormFile(stream, 0, bytes.Length, "file", fileName);
            return file;
        }
    }
}
