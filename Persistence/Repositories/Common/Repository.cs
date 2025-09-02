using Application.Contracts.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UrlShortenerDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(UrlShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> Exists(T entity)
        {
            var exists = await _dbSet.ContainsAsync(entity);
            return exists ? entity : null!;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
