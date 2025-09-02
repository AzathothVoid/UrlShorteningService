using Application.Contracts.Persistence.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IURLTokenRepository : IRepository<URLToken>
    {
        Task<URLToken?> GetByTokenAsync(string token);
        Task<bool> IsTokenUniqueAsync(string token);
        Task<bool> IsUrlValid(URLToken urlToken);
    }
}
