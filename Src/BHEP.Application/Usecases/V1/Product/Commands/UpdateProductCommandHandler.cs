using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Product;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;

namespace BHEP.Application.Usecases.V1.Product.Commands;
public sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand, Responses.ProductResponse>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly IBlobStorageService blobStorageRepository;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
    }

    public async Task<Result<Responses.ProductResponse>> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productExist = await productRepository.FindByIdAsync(request.Id, cancellationToken)
                    ?? throw new ProductException.ProductIdNotFoundException();

        var oldImageUrl = productExist.Image;

        var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
        var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
        var newImageUrl = await blobStorageRepository.UploadFormFormFile(request.Image, nameImage, "service")
             ?? throw new Exception("Upload File fail");

        try
        {
            productExist.Update(
                request.Name,
                newImageUrl,
                request.Description,
                request.Price,
                request.Stock
                );

            productRepository.Update(productExist);

            if (string.IsNullOrEmpty(oldImageUrl))
                blobStorageRepository.DeleteBlobSnapshotsAsync(oldImageUrl);

            var resultResponse = mapper.Map<Responses.ProductResponse>(productExist);
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
