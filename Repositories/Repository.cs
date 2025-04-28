
using Api.SM.Data;
using Microsoft.EntityFrameworkCore;
namespace Api.SM.Repository;
public interface IRepsitory<T>
{
    Task<IEnumerable<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);

    Task<T?> FindAsync(params object[] ps);
}

public abstract class Repository<T> : IRepsitory<T>
    where T : class
{
    private readonly DataContext _context; // Ensure DataContext is a class, not a namespace
    protected readonly DbSet<T> _dbSet;

    public Repository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>(); // Correctly initialize _dbSet using the context
    }

    public virtual async Task<IEnumerable<T>?> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<T?> FindAsync(params object[] ps)
    {
        return await _dbSet.FindAsync(ps);
    }
}

