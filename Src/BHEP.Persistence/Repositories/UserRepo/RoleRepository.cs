using System.Linq.Expressions;
using BHEP.Domain.Entities.UserEntities;
using BHEP.Persistence.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
﻿using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Persistence.Repositories.UserRepo;
public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
{
    private readonly ApplicationDbContext context;
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<bool> IsAdmin(int roleId)
    {
        var role = await context.Role.FindAsync(roleId);
        return role.Name.ToLower().Equals("admin");
    }

    public IQueryable<Role> FindAll(Expression<Func<Role, bool>>? predicate = null)
    {
        IQueryable<Role> items = context.Set<Role>().AsNoTracking();
        if (predicate != null)
            items = items.Where(predicate);
        return items;
    }

    public async Task<Role?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Set<Role>().FindAsync(new object[] { id }, cancellationToken);
    }
}
