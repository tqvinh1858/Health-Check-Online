using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Blog;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;

namespace BHEP.Application.Usecases.V1.Blog.Commands;
public sealed class DeleteBlogCommandHandler : ICommandHandler<Command.DeleteBlogCommand>
{
    private readonly IBlogRepository blogRepository;
    private readonly IBlogPhotoRepository blogPhotoRepository;
    private readonly IBlobStorageService blobStorageRepository;
    public DeleteBlogCommandHandler(
        IBlogRepository blogRepository,
        IBlobStorageService blobStorageRepository,
        IBlogPhotoRepository blogPhotoRepository)
    {
        this.blogRepository = blogRepository;
        this.blobStorageRepository = blobStorageRepository;
        this.blogPhotoRepository = blogPhotoRepository;
    }

    public async Task<Result> Handle(Command.DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var BlogExist = await blogRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new BlogException.BlogIdNotFoundException();

        try
        {
            //Get urlImages
            var listPhotos = await blogRepository.GetPhotos(request.Id);
            var urlImages = listPhotos.Select(x => x.Image).ToList();

            // Delete BlogPhoto
            if (listPhotos.Any())
                blogPhotoRepository.RemoveMultiple(listPhotos);

            // Delete Blog
            blogRepository.Remove(BlogExist);

            // Delete ListPhotos
            if (urlImages.Any())
                await blobStorageRepository.DeleteMultiBlobAsync(urlImages);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
