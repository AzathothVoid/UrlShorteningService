using Application.DTOs.URLToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IURLTokenService
    {
        Task<string> GenerateUniqueTokenAsync(int length = 6);
        Task<CreateURLTokenDto> CreateShortUrlAsync(string originalUrl, int tokenLength = 6, DateTime? expiresAt = null);      
    }
}
