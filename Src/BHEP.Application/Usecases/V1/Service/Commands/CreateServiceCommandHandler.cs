using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Service;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using BHEP.Persistence;
using Microsoft.AspNetCore.Http;

namespace BHEP.Application.Usecases.V1.Service.Commands;
public sealed class CreateServiceCommandHandler : ICommandHandler<Command.CreateServiceCommand, Responses.ServiceResponse>
{
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageRepository;
    private readonly IServiceRepository serviceRepository;
    private readonly ApplicationDbContext context;

    public CreateServiceCommandHandler(
        IMapper mapper,
        IBlobStorageService blobStorageRepository,
        IServiceRepository serviceRepository,
        ApplicationDbContext context)
    {
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
        this.serviceRepository = serviceRepository;
        this.context = context;
    }

    public async Task<Result<Responses.ServiceResponse>> Handle(Command.CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var ServiceNameExist = serviceRepository.FindAll(x => x.Name == request.Name)
            ?? throw new ServiceException.ServiceBadRequestException("Service name already exists.");

        var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;

        var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
        var imageUrl = await blobStorageRepository.UploadFormFormFile(request.Image, nameImage, "service")
            ?? throw new Exception("Upload File fail");


        var Service = new Domain.Entities.SaleEntities.Service
        {
            Name = request.Name,
            Image = imageUrl,
            Type = request.Type,
            Description = request.Description,
            Price = request.Price,
            Duration = request.Duration,
        };

        try
        {
            serviceRepository.Add(Service);
            await context.SaveChangesAsync();
            var resultResponse = mapper.Map<Responses.ServiceResponse>(Service);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(imageUrl))
                await blobStorageRepository.DeleteBlobSnapshotsAsync(imageUrl);
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
