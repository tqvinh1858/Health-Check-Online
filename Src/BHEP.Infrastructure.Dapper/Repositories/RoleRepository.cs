using BHEP.Domain.Entities.UserEntities;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;
using BHEP.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BHEP.Infrastructure.Dapper.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext context;

    public RoleRepository(
        ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<int> AddAsync(Role entity)
    {
        var sql = "Insert Into Role (Name,Description) VALUES (@Name,@Description)";
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var result = await context.Database.GetDbConnection()
                .ExecuteAsync(sql, entity, transaction.GetDbTransaction());

            await transaction.CommitAsync();

            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM Role WHERE Id = @Id";

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var result = await context.Database.GetDbConnection()
                .ExecuteAsync(sql, new { Id = id }, transaction.GetDbTransaction());

            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync()
    {
        var sql = "SELECT Id, Name, Description FROM Role";
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var result = await context.Database.GetDbConnection()
                .QueryAsync<Role>(sql, transaction.GetDbTransaction());
            await transaction.CommitAsync();

            return result.ToList();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        var sql = "SELECT Id, Name, Description FROM Role WHERE Id = @Id";
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var result = await context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<Role>(sql, new { Id = id }, transaction.GetDbTransaction());
            await transaction.CommitAsync();

            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<int> UpdateAsync(Role entity)
    {
        var sql = "UPDATE Role SET Name = @Name, Description = @Description WHERE Id = @Id";
        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var result = await context.Database.GetDbConnection()
                .ExecuteAsync(sql, entity, transaction.GetDbTransaction());

            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<bool> IsRoleExist(string name)
    {
        var sql = "SELECT COUNT(*) FROM Role WHERE Name = @Name";
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var count = await context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<int>(sql, new { Name = name }, transaction.GetDbTransaction());
            await transaction.CommitAsync();

            return count > 0;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message, ex);
        }
    }
}
