using BHEP.Domain.Entities.PostEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IPostSpecialistRepository
{
    Task AddMulti(List<PostSpecialist> postSpecialists);
}
