using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Service;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;

namespace BHEP.Application.Usecases.V1.Service.Commands;
public sealed class UpdateServiceCommandHandler : ICommandHandler<Command.UpdateServiceCommand, Responses.ServiceResponse>
{
    private readonly IServiceRepository serviceRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageRepository;


    public UpdateServiceCommandHandler(
        IServiceRepository serviceRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository)
    {
        this.serviceRepository = serviceRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<Responses.ServiceResponse>> Handle(Command.UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceExist = await serviceRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException.ServiceIdNotFoundException();


        var oldImageUrl = serviceExist.Image;

        var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
        var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
        var newImageUrl = await blobStorageRepository.UploadFormFormFile(request.Image, nameImage, "service")
             ?? throw new Exception("Upload File fail");

        try
        {
            serviceExist.Update(
                request.Name,
                newImageUrl,
                request.Type,
                request.Description,
                request.Price,
                request.Duration
                );

            serviceRepository.Update(serviceExist);

            // Delete oldImage In BlobStorage
            if (string.IsNullOrEmpty(oldImageUrl))
                blobStorageRepository.DeleteBlobSnapshotsAsync(oldImageUrl);

            var resultResponse = mapper.Map<Responses.ServiceResponse>(serviceExist);
            return Result.Success(resultResponse);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(newImageUrl))
                await blobStorageRepository.DeleteBlobSnapshotsAsync(newImageUrl);
            throw new Exception(ex.Message);
        }
    }
}
