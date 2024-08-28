using BHEP.Domain.Entities.UserEntities;
using BHEP.Infrastructure.Dapper.Repositories.Interfaces;
using BHEP.Persistence;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace DemoCICD.Infrastructure.Dapper.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration configuration;
    private readonly ApplicationDbContext context;

    public UserRepository(
        IConfiguration configuration,
        ApplicationDbContext context)
    {
        this.configuration = configuration;
        this.context = context;
    }

    #region Using Transaction In Dapper

    // using transaction with Dapper
    public async Task<int> AddAsyncV2(User entity)
    {
        var sql = "Insert Into User (Id,Name,Price,Description) VALUES (@Id,@Name,@Price,@Description)";
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

    public async Task<IReadOnlyList<User>> GetAllAsyncV2()
    {
        var sql = "SELECT Id, Name, Price, Description FROM User";

        // Start Transaction
        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // Execute Query
            var result = await context.Database.GetDbConnection()
            .QueryAsync<User>(sql, transaction: transaction.GetDbTransaction());

            // Commit Transaction
            await transaction.CommitAsync();

            return result.ToList();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }

    #endregion

    public async Task<int> AddAsync(User entity)
    {
        var sql = "Insert Into User (RoleId,GeoLocationId,SpecialistId,FullName,Email,HashPassword,PhoneNumber,Gender,Description,Avatar,IsActive) " +
            "VALUES (@RoleId,@GeoLocationId,@SpecialistId,@FullName,@Email,@HashPassword,@PhoneNumber,@Gender,@Description,@Avatar,@IsActive)";
        using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }


    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM User WHERE Id = @Id";
        using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result;
        }
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        var sql = "SELECT Id, Name, Price, Description FROM User";
        using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<User>(sql);
            return result.ToList();
        }
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var sql = @"
        SELECT 
            u.Id,
            u.GeoLocationId,
            r.RoleName,
            u.FullName,
            u.Email,
            u.PhoneNumber,
            u.Gender,
            u.Description,
            u.Avatar,
            u.IsActive
        FROM 
            Users u
        INNER JOIN 
            Roles r ON u.RoleId = r.Id
        WHERE 
            u.Id = @UserId;
        
        SELECT 
            a.Id,
            a.CustomerId,
            a.EmployeeId,
            a.AppointmentTime,
            a.Description
        FROM 
            Appointments a
        WHERE 
            a.CustomerId = @UserId OR a.EmployeeId = @UserId;";
        using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
            return result;
        }
    }

    public async Task<int> UpdateAsync(User entity)
    {
        var sql = "UPDATE User SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id";
        using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectionStrings")))
        {
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, entity);
            return result;
        }
    }
}
