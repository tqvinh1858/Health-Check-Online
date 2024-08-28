using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Blog;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.BlogEntities;
using BHEP.Domain.Exceptions;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using BHEP.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.Blog.Commands;
public sealed class UpdateBlogCommandHandler : ICommandHandler<Command.UpdateBlogCommand, Responses.BlogResponse>
{
    private readonly IBlogRepository BlogRepository;
    private readonly IBlobStorageService blobStorageRepository;
    private readonly IBlogPhotoRepository blogPhotoRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly Random random;
    private int count;
    public UpdateBlogCommandHandler(
        IBlogRepository BlogRepository,
        IMapper mapper,
        IBlobStorageService blobStorageRepository,
        IBlogPhotoRepository blogPhotoRepository,
        ApplicationDbContext context)
    {
        this.BlogRepository = BlogRepository;
        this.mapper = mapper;
        this.blobStorageRepository = blobStorageRepository;
        this.blogPhotoRepository = blogPhotoRepository;
        random = new Random();
        count = 0;
        this.context = context;
    }
    public async Task<Result<Responses.BlogResponse>> Handle(Command.UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var Blog = await BlogRepository.FindByIdAsync(request.Id.Value)
            ?? throw new BlogException.BlogIdNotFoundException();

        List<string> listImages = new List<string>();

        try
        {
            // UpdateBlogTopics
            await UpdateBlogTopics(request.Id.Value, request.Topics, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            var listTopics = await BlogRepository.GetTopics(Blog.Id);

            // Update BlogPhotos
            listImages = await UpdateBlogPhotos(request.Id.Value, request.Photos);

            // Update Blog
            Blog.Update(
                request.Title,
                request.Content,
                request.Status);
            BlogRepository.Update(Blog);

            await context.SaveChangesAsync();
            var listPhotos = await BlogRepository.GetPhotos(Blog.Id);
            // Convert To Response
            var listTopicMapper = mapper.Map<List<Responses.TopicResponse>>(listTopics);
            var listPhotoMapper = mapper.Map<List<Responses.PhotoResponse>>(listPhotos);

            var resultResponse = new Responses.BlogResponse(
                    Blog.Id,
                    Blog.UserId,
                    Blog.Title,
                    Blog.Content,
                    Blog.Status,
                    listTopicMapper,
                    listPhotoMapper
                    );

            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            await blobStorageRepository.DeleteMultiBlobAsync(listImages);
            throw new Exception(ex.Message);
        }
    }


    private async Task UpdateBlogTopics(int blogId, ICollection<int> topics, CancellationToken cancellationToken)
    {
        // Find Topics to Remove
        var topicsToRemove = await context.BlogTopic.Where(x => x.BlogId == blogId && !topics.Contains(x.TopicId)).ToListAsync(cancellationToken);
        if (topicsToRemove.Count > 0)
        {
            context.BlogTopic.RemoveRange(topicsToRemove);
            await context.SaveChangesAsync(cancellationToken);
        }

        // Find Topics to Add
        if (topics != null && topics.Count > 0)
        {
            var existingTopicIds = await context.BlogTopic.Where(x => x.BlogId == blogId).Select(bt => bt.TopicId).ToListAsync();
            var topicsToAdd = topics.Where(topic => !existingTopicIds.Contains(topic))
                .Select(topic => new BlogTopic
                {
                    BlogId = blogId,
                    TopicId = topic,
                });
            if (topicsToAdd != null)
                await context.BlogTopic.AddRangeAsync(topicsToAdd);
        }

    }

    private async Task<List<string>> UpdateBlogPhotos(int blogId, ICollection<IFormFile> photos)
    {
        List<string> listImages = new List<string>();
        // GetListPhotos
        var listPhotos = await context.BlogPhoto.Where(x => x.BlogId == blogId).ToListAsync();
        // Remove All BlogPhotos
        blogPhotoRepository.RemoveMultiple(listPhotos);

        // Remove All Photos In Blob
        await blobStorageRepository.DeleteMultiBlobAsync(listPhotos.Select(x => x.Image).ToList());
        // Add new List Photos
        if (photos is null || photos.Count <= 0)
            return listImages;
        foreach (var image in photos)
        {
            string fileName = $"{blogId}-{random.Next(0, int.MaxValue)}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";
            string urlString = await blobStorageRepository.UploadFormFormFile(image, fileName, "Blogs");

            var blogPhoto = new Domain.Entities.BlogEntities.BlogPhoto
            {
                BlogId = blogId,
                Image = urlString,
                ONum = count++
            };

            blogPhotoRepository.Add(blogPhoto);
            listImages.Add(urlString);
        }

        return listImages;
    }
}
