using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Persistence.Repositories.PostRepo;
public class PostSpecialistRepository : IPostSpecialistRepository
{
    private readonly ApplicationDbContext context;

    public PostSpecialistRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AddMulti(List<PostSpecialist> postSpecialists)
        => await context.PostSpecialist.AddRangeAsync(postSpecialists);
}
