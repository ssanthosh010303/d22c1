using Microsoft.EntityFrameworkCore;

using EmployeeRequestTracker.Models;

namespace EmployeeRequestTracker.Repository;

public interface IRepository<TKey, TEntity> where TEntity : class
{
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<TEntity> Delete(TKey key);
    Task<TEntity> Get(TKey key);
    Task<IList<TEntity>> GetAll();
}

public abstract class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
{
    protected readonly DbContext _context;

    protected Repository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual async Task<TEntity> Add(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> Delete(TKey key)
    {
        var entity = await _context.Set<TEntity>().FindAsync(key);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        return entity;
    }

    public virtual async Task<TEntity> Get(TKey key)
    {
        return await _context.Set<TEntity>().FindAsync(key);
    }

    public virtual async Task<IList<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }
}
