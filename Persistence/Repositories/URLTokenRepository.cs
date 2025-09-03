using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;

namespace Persistence.Repositories
{
    public class URLTokenRepository : Repository<URLToken>, IURLTokenRepository
    {
        private readonly UrlShortenerDbContext _dbContext;

        public URLTokenRepository(UrlShortenerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<URLToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.Set<URLToken>()
                .FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task<bool> IsTokenUniqueAsync(string token)
        {
            return !await _dbContext.Set<URLToken>()
                .AnyAsync(t => t.Token == token);
        }

        public async Task<bool> TokenExistsAsync(string token)
        {
            return await _dbContext.Set<URLToken>()
                .AnyAsync(t => t.Token == token);
        }

        public async Task<bool> IsUrlValid(URLToken urlToken)
        {
            if (urlToken == null)
                return false;
            if (urlToken.ExpiresAt.HasValue && urlToken.ExpiresAt.Value < DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task<int> UpdateTokenClick(URLToken entity)
        {

            entity.Clicks = entity.Clicks + 1;
            _dbContext.Set<URLToken>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Clicks;
        }
    }
}
