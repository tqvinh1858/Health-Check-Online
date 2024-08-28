using BHEP.Domain.Entities.AppointmentEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface ISpecialistRepository : IRepositoryBase<Specialist, int>
{
    Task<List<Specialist>> GetSpecialistsFromListId(List<int> listId);
}
