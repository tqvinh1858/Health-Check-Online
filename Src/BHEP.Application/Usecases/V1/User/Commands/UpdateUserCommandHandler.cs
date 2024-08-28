using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using Microsoft.AspNetCore.Http;

namespace BHEP.Application.Usecases.V1.User.Commands;
public sealed class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUserCommand, Responses.UserResponse>
{
    private readonly IUserRepository CustomerRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageRepository;
    public UpdateUserCommandHandler(
        IUserRepository CustomerRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository)
    {
        this.CustomerRepository = CustomerRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var CustomerExist = await CustomerRepository.FindByIdAsync(request.Id.Value)
            ?? throw new UserException.UserNotFoundException();
        string oldImageUrl = string.Empty;
        string newImageUrl = string.Empty;
        if (request.Avatar != null)
        {
            oldImageUrl = CustomerExist.Avatar;
            string[] parts = request.Email.Split('@');
            var nameImage = $"{parts[0]}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

            newImageUrl = await blobStorageRepository.UploadFormFormFile(request.Avatar, nameImage, "avatars");
        }


        try
        {
            CustomerExist.FullName = request.FullName;
            CustomerExist.Email = request.Email;
            CustomerExist.PhoneNumber = request.PhoneNumber;
            CustomerExist.Gender = request.Gender;
            CustomerExist.Avatar = string.IsNullOrEmpty(newImageUrl) ? CustomerExist.Avatar : newImageUrl;

            CustomerRepository.Update(CustomerExist);
            var CustomerResponse = mapper.Map<Responses.UserResponse>(CustomerExist);


            // Delete oldImage In BlobStorage
            if (string.IsNullOrEmpty(oldImageUrl))
                blobStorageRepository.DeleteBlobSnapshotsAsync(oldImageUrl);

            return Result.Success(CustomerResponse, 202);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(newImageUrl))
                await blobStorageRepository.DeleteBlobSnapshotsAsync(newImageUrl);
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
