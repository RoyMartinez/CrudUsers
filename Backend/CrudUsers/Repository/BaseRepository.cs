using Microsoft.EntityFrameworkCore;
using CrudUsers.Context;
using CrudUsers.Shared;

namespace CrudUsers.Repository;
public interface IBaseRepository<T>
{
    public Task<PagedResult<T>> GetAllAsync(int? pageNumber, int? pageSize);

    public Task<T?> GetByIdAsync(int id);

    public Task<T> CreateAsync(T entity);

    public Task<T> UpdateAsync(T entity);

    public Task<bool> DeleteAsync(int id);
}
public abstract class BaseRepository<T>(IDbContextFactory<DatabaseContext> dbContextFactory, ILogger<BaseRepository<T>> logger) : IBaseRepository<T> where T : class
{
    protected readonly IDbContextFactory<DatabaseContext> _dbContextFactory = dbContextFactory;
    protected readonly ILogger<BaseRepository<T>> _logger = logger;

    protected DatabaseContext CreateDbContext() => _dbContextFactory.CreateDbContext();

    public virtual async Task<PagedResult<T>> GetAllAsync(int? pageNumber, int? pageSize)
    {
        try
        {
            using var context = CreateDbContext();
            var query = context.Set<T>();
            return await query.ToPagedResultAsync(pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            string TypeofName = typeof(T).Name;
            _logger.LogError(ex, "An error occurred while trying to retrieve {TypeofName} entities.", TypeofName);
            throw new FetchException("An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            using var context = CreateDbContext();
            T? entity = await context.Set<T>().FindAsync(id);
            return entity;
        }
        catch (Exception ex)
        {
            string TypeofName = typeof(T).Name;
            _logger.LogError(ex, "An error occurred while trying to retrieve {TypeofName} entities.", TypeofName);
            throw new FetchException("An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }

    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            using var context = CreateDbContext();
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            string TypeofName = typeof(T).Name;
            _logger.LogError(ex, "An error occurred while trying to retrieve {TypeofName} entities.", TypeofName);
            throw new FetchException("An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            using var context = CreateDbContext();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            string TypeofName = typeof(T).Name;
            _logger.LogError(ex, "An error occurred while trying to retrieve {TypeofName} entities.", TypeofName);
            throw new FetchException("An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using var context = CreateDbContext();
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            string TypeofName = typeof(T).Name;
            _logger.LogError(ex, "An error occurred while trying to retrieve {TypeofName} entities.", TypeofName);
            throw new FetchException("An error occurred while trying to retrieve {typeof(T).Name} entities.");
        }
    }
}