using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Product;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.Product.Commands;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand, Responses.ProductResponse>
{
    private readonly IProductRepository productRepository;
    private readonly IBlobStorageService blobStorageRepository;
    private readonly IMapper mapper;
    private readonly ApplicationDbContext context;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IBlobStorageService blobStorageRepository,
        IMapper mapper,
        ApplicationDbContext context)
    {
        this.productRepository = productRepository;
        this.blobStorageRepository = blobStorageRepository;
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<Result<Responses.ProductResponse>> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productNameExist = productRepository.FindAll(x => x.Name == request.Name)
                 ?? throw new ProductException.ProductBadRequestException("Product name already exists.");

        var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;

        var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
        var imageUrl = await blobStorageRepository.UploadFormFormFile(request.Image, nameImage, "service")
            ?? throw new Exception("Upload File fail");


        var product = new Domain.Entities.SaleEntities.Product
        {
            Name = request.Name,
            Image = imageUrl,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock
        };

        try
        {
            productRepository.Add(product);
            await context.SaveChangesAsync();
            var resultResponse = mapper.Map<Responses.ProductResponse>(product);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(imageUrl))
                await blobStorageRepository.DeleteBlobAsync(imageUrl);
            throw new Exception(ex.Message);
        }
    }
}
