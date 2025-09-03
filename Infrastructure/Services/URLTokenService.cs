using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.DTOs.URLToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class URLTokenService : IURLTokenService
    {
        private readonly IURLTokenRepository _urlTokenRepository;
        private readonly Random _rng = new();
        private const string _alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public URLTokenService(IURLTokenRepository urlTokenRepository)
        {
            _urlTokenRepository = urlTokenRepository;
        }

        public async Task<string> GenerateUniqueTokenAsync(int length = 6)
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                var token = GenerateRandomToken(length);
                if (!await _urlTokenRepository.TokenExistsAsync(token)) return token;
            }

            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("=", "")
            .Replace("+", "-")
            .Replace("/", "_")
            .Substring(0, Math.Min(10, length));
        }


        private string GenerateRandomToken(int length)
        {
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                sb.Append(_alphabet[_rng.Next(_alphabet.Length)]);
            return sb.ToString();
        }

        public async Task<CreateURLTokenDto> CreateShortUrlAsync(string originalUrl, int tokenLength = 6, DateTime? expiresAt = null)
        {
            var token = await GenerateUniqueTokenAsync(tokenLength);
            var entity = new CreateURLTokenDto { Token = token, OriginalUrl = originalUrl, ExpiresAt = expiresAt };
            return entity;
        }
    }
}
