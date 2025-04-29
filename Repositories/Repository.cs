using Api.SM.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.SM.Repository
{
    public interface IRepsitory<T>
    {
        Task<IEnumerable<T>> GetAllRAsync(Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<T?> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<T?> FindAsync(params object[] ps);
    }

    public abstract class Repository<T> : IRepsitory<T>
        where T : class
    {
        private readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllRAsync(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);
            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> CreateAsync(T entity)
        {

            var item= await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            var item= _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            var item= _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return item ==null?false:true;
        }

        public virtual async Task<T?> FindAsync(params object[] ps)
        {
            return await _dbSet.FindAsync(ps);
        }
    }
}
