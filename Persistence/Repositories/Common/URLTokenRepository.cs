using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Common
{
    public class URLTokenRepository : Repository<URLToken>, IURLTokenRepository
    {
        private readonly DbContext _dbContext;

        public URLTokenRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<URLToken?> GetByTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTokenUniqueAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUrlValid(URLToken urlToken)
        {
            throw new NotImplementedException();
        }
    }
}
