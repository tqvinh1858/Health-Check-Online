using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Blog;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using BHEP.Persistence;
using Microsoft.AspNetCore.Http;

namespace BHEP.Application.Usecases.V1.Blog.Commands;
public sealed class CreateBlogCommandHandler : ICommandHandler<Command.CreateBlogCommand, Responses.BlogResponse>
{
    private readonly IBlogRepository BlogRepository;
    private readonly IBlobStorageService blobStorageRepository;
    private readonly IBlogTopicRepository blogTopicRepository;
    private readonly IBlogPhotoRepository blogPhotoRepository;
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateBlogCommandHandler(
        IBlogRepository BlogRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository,
        IUserRepository userRepository,
        IBlogTopicRepository blogTopicRepository,
        IBlogPhotoRepository blogPhotoRepository,
        ApplicationDbContext context)
    {
        this.BlogRepository = BlogRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
        this.userRepository = userRepository;
        this.blogTopicRepository = blogTopicRepository;
        this.blogPhotoRepository = blogPhotoRepository;
        this.context = context;
    }

    public async Task<Result<Responses.BlogResponse>> Handle(Command.CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId)
            ?? throw new Domain.Exceptions.UserException.UserNotFoundException();


        var Blog = new Domain.Entities.BlogEntities.Blog
        {
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            Status = Contract.Enumerations.BlogStatus.Pending,
        };

        List<string> listImages = new List<string>();
        try
        {
            BlogRepository.Add(Blog);
            await context.SaveChangesAsync();

            // Add BlogTopic
            await blogTopicRepository.Add(Blog.Id, request.Topics);
            await context.SaveChangesAsync();
            var listTopics = await BlogRepository.GetTopics(Blog.Id);

            // Add BlogPhoto
            listImages = await AddPhotoToBlobStorage(Blog.Id, request.Photos);
            await context.SaveChangesAsync();
            var listPhotos = await BlogRepository.GetPhotos(Blog.Id);

            // ConvertBlogResponse
            var resultResponse = new Responses.BlogResponse(
                Blog.Id,
                Blog.UserId,
                Blog.Title,
                Blog.Content,
                Blog.Status,
                mapper.Map<List<Responses.TopicResponse>>(listTopics),
                mapper.Map<List<Responses.PhotoResponse>>(listPhotos));
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            await blobStorageRepository.DeleteMultiBlobAsync(listImages);
            throw new Exception(ex.Message);
        }
    }

    private async Task<List<string>> AddPhotoToBlobStorage(int blogId, ICollection<IFormFile> photos)
    {
        List<string> listImages = new List<string>();
        Random random = new Random();
        int count = 0;
        foreach (var image in photos)
        {
            string fileName = $"{blogId}-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}";
            string urlString = await blobStorageRepository.UploadFormFormFile(image, fileName, "Blogs");

            var blogPhoto = new Domain.Entities.BlogEntities.BlogPhoto
            {
                BlogId = blogId,
                Image = urlString,
                ONum = count++
            };

            blogPhotoRepository.Add(blogPhoto);
            listImages.Add(fileName);
        }

        return listImages;
    }
}
