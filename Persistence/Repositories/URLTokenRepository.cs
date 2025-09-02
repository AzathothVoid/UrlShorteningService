using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class URLTokenRepository : Repository<URLToken>, IURLTokenRepository
    {
        private readonly DbContext _dbContext;

        public URLTokenRepository(DbContext dbContext) : base(dbContext)
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

        public async Task<bool> IsUrlValid(URLToken urlToken)
        {
            if (urlToken == null)
                return false;
            if (urlToken.ExpiresAt.HasValue && urlToken.ExpiresAt.Value < DateTime.UtcNow)
                return false;

            return true;
        }
    }
}
