using System.Linq.Expressions;
using BHEP.Domain.Abstractions.EntityBase;
using BHEP.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Persistence.Repositories;
public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : EntityBase<TKey>
{

    private readonly ApplicationDbContext context;

    public RepositoryBase(ApplicationDbContext context)
        => this.context = context;

    public void Dispose()
        => context?.Dispose();

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = context.Set<TEntity>().AsNoTracking(); // Importance Always include AsNoTracking for Query Side
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items = items.Where(predicate);

        return items;
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);

    public void Add(TEntity entity)
        => context.Add(entity);

    public void AddMultiple(List<TEntity> entities)
    => context.Set<TEntity>().AddRange(entities);

    public void Remove(TEntity entity)
        => context.Set<TEntity>().Remove(entity);

    public void RemoveMultiple(List<TEntity> entities)
        => context.Set<TEntity>().RemoveRange(entities);

    public void Update(TEntity entity)
        => context.Set<TEntity>().Update(entity);

    public void UpdateMultiple(List<TEntity> entities)
    => context.Set<TEntity>().UpdateRange(entities);
}
