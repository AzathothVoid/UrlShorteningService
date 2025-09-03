using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;


namespace Persistence.Repositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private readonly UrlShortenerDbContext _dbContext;
        private readonly DbSet<Visit> _dbSet;

        public VisitRepository(UrlShortenerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Visit>();
        }

        public async Task AddBatchAsync(List<Visit> batch, CancellationToken stoppingToken)
        {
            _dbSet.AddRange(batch);
            await _dbContext.SaveChangesAsync(stoppingToken);
        }

        public async Task<long> GetTotalVisitsAsync(int? URLTokenId)
        {
            if (URLTokenId.HasValue)
                return await _dbSet.LongCountAsync(v => v.URLTokenId == URLTokenId.Value);
            else
                return await _dbSet.LongCountAsync();
        }

        public async Task<long> GetUniqueVisitorsByIpAsync(int? URLTokenId)
        {
            var query = _dbSet.AsQueryable();

            if (URLTokenId.HasValue)
            {
                query = query.Where(v => v.URLTokenId == URLTokenId.Value);
            }

            return await query.Select(v => v.IpAddress)
                             .Distinct()
                             .LongCountAsync();
        }

        public async Task<List<(DateTimeOffset date, int count)>> GetDailyCountsAsync(int? URLTokenId, int days = 30)
        {
            var from = DateTimeOffset.UtcNow.AddDays(-days);
            var query = _dbSet.AsQueryable();

            if (URLTokenId.HasValue)
            {
                query = query.Where(v => v.URLTokenId == URLTokenId.Value && v.Timestamp >= from);
            }
            else
            {
                query = query.Where(v => v.Timestamp >= from);
            }

            var visits = await query
                .Select(v => new { v.Timestamp })
                .ToListAsync();

            var result = visits
                .GroupBy(v => v.Timestamp.Date)
                .Select(g => new { Date = new DateTimeOffset(g.Key, TimeSpan.Zero), Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToList();

            return result.Select(x => (date: x.Date, count: x.Count)).ToList();
        }

        public async Task<List<(string Referrer, int Count)>> GetTopReferrersAsync(int? URLTokenId, int topN = 10)
        {
            var query = _dbSet.AsQueryable();

            if (URLTokenId.HasValue)
            {
                query = query.Where(v => v.URLTokenId == URLTokenId.Value && !string.IsNullOrEmpty(v.Referrer));
            }

            var result = await query
                .GroupBy(v => v.Referrer)
                .Select(g => new { Referrer = g.Key!, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topN)
                .ToListAsync();

            return result.Select(x => (x.Referrer, x.Count)).ToList();
        }

        public async Task<List<(string label, int count)>> GetDeviceBreakdownAsync(int? URLTokenId = null)
        {
            var query = _dbSet.AsQueryable();

            if (URLTokenId.HasValue)
            {
                query = query.Where(v => v.URLTokenId == URLTokenId.Value);
            }

            var result = await query
                .GroupBy(v => v.DeviceType ?? "Unknown")
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return result.Select(x => (x.Label, x.Count)).ToList();
        }

        public async Task<List<(string label, int count)>> GetBrowserBreakdownAsync(int? URLTokenId = null)
        {
            var query = _dbSet.AsQueryable();

            if (URLTokenId.HasValue)
            {
                query = query.Where(v => v.URLTokenId == URLTokenId.Value);
            }

            var result = await query
                .GroupBy(v => v.Browser ?? "Unknown")
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return result.Select(x => (x.Label, x.Count)).ToList();
        }

        public async Task<List<(DateTime timestamp, string ip, string ua, string referrer)>> GetRecentVisitsAsync(int? URLTokenId = null, int limit = 20)
        {
            var query = _dbSet.AsQueryable();

            if (URLTokenId.HasValue)
            {
                query = query.Where(v => v.URLTokenId == URLTokenId.Value);
            }

            var result = await query
                .OrderByDescending(v => v.Timestamp)
                .Take(limit)
                .Select(v => new {
                    v.Timestamp,
                    v.IpAddress,
                    v.UserAgent,
                    v.Referrer
                })
                .ToListAsync();

            return result.Select(x => (
                timestamp: x.Timestamp.DateTime,
                ip: x.IpAddress ?? "Unknown",
                ua: x.UserAgent ?? "Unknown",
                referrer: x.Referrer ?? "Direct"
            )).ToList();
        }

        public async Task<List<(int id, string token)>> ListURLTokensAsync(int limit = 50)
        {
            var result = await _dbContext.Set<URLToken>()
                .Where(u => _dbSet.Any(v => v.URLTokenId == u.Id))
                .Select(u => new { u.Id, u.Token })
                .OrderBy(u => u.Id)
                .Take(limit)
                .ToListAsync();

            return result.Select(x => (id: x.Id, token: x.Token)).ToList();
        }
    }
}
