using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Post;
using BHEP.Domain.Entities.PostEntities;
using BHEP.Persistence;
using BHEP.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.Post.Commands;
public sealed class UpdatePostCommandHandler : ICommandHandler<Command.UpdatePostCommand, Responses.PostResponse>
{
    private readonly IPostRepository postRepository;
    private readonly ApplicationDbContext context;

    public UpdatePostCommandHandler(
        IPostRepository postRepository,
        ApplicationDbContext context)
    {
        this.postRepository = postRepository;
        this.context = context;
    }

    public async Task<Result<Responses.PostResponse>> Handle(Command.UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.FindByIdAsync(request.Id.Value);
        if (post == null)
            return Result.Failure<Responses.PostResponse>("Post not found");

        post.Title = request.Title;
        post.Content = request.Content;
        post.Age = request.Age;
        post.Gender = request.Gender;
        post.Status = request.Status;

        try
        {
            postRepository.Update(post);
            await UpdatePostSpecialists(request.Id.Value, request.Specialists, cancellationToken);

            var resultResponse = new Responses.PostResponse(
                       post.Id,
                       post.UserId,
                       post.Title,
                       post.Content,
                       post.Age,
                       post.Gender,
                       post.Status
                       );

            return Result.Success(resultResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    private async Task UpdatePostSpecialists(int postId, ICollection<int> specialists, CancellationToken cancellationToken)
    {
        // Find specialists to Remove
        var specialistsToRemove = await context.PostSpecialist.Where(x => x.PostId == postId && !specialists.Contains(x.SpecialistId)).ToListAsync(cancellationToken);
        if (specialistsToRemove.Count > 0)
        {
            context.PostSpecialist.RemoveRange(specialistsToRemove);
            await context.SaveChangesAsync(cancellationToken);
        }

        // Find specialists to Add
        if (specialists != null && specialists.Count > 0)
        {
            var existingspecialistIds = await context.PostSpecialist.Where(x => x.PostId == postId).Select(bt => bt.SpecialistId).ToListAsync();
            var specialistsToAdd = specialists.Where(specialist => !existingspecialistIds.Contains(specialist))
                .Select(specialist => new PostSpecialist
                {
                    PostId = postId,
                    SpecialistId = specialist,
                });
            if (specialistsToAdd != null)
                await context.PostSpecialist.AddRangeAsync(specialistsToAdd);
        }

    }
}
