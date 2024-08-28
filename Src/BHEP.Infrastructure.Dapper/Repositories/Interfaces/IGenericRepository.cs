namespace BHEP.Infrastructure.Dapper.Repositories.Interfaces;
public interface IGenericRepository<TEntity, TType>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TType id);

    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task<int> AddAsync(TEntity entity);

    Task<int> UpdateAsync(TEntity entity);

    Task<int> DeleteAsync(TType id);
}
