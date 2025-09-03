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

        public async Task<long> GetTotalVisitsAsync(int URLTokenId)
        {
           return  await _dbSet.LongCountAsync(v => v.URLTokenId == URLTokenId);

        }

        public async Task<long> GetUniqueVisitorsByIpAsync(int URLTokenId)
        {
           return  await _dbSet.Where(v => v.URLTokenId == URLTokenId)
                               .Select(v => v.IpAddress)
                               .Distinct()
                               .LongCountAsync();
        }

        public async Task<List<(DateTimeOffset date, int count)>> GetDailyCountsAsync(int URLTokenId, int days = 30)
        {
            var from = DateTimeOffset.UtcNow.AddDays(-days);
            var result = await _dbSet
                .Where(v => v.URLTokenId == URLTokenId && v.Timestamp >= from)
                .GroupBy(v => v.Timestamp.Date)
                .Select(g => new { Date = new DateTimeOffset(g.Key, TimeSpan.Zero), Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return result.Select(x => (date: x.Date, count: x.Count)).ToList();
        }

        public async Task<List<(string Referrer, int Count)>> GetTopReferrersAsync(int URLTokenId, int topN = 10)
        {
            var result = await _dbSet
                .Where(v => v.URLTokenId == URLTokenId && !string.IsNullOrEmpty(v.Referrer))
                .GroupBy(v => v.Referrer)
                .Select(g => new { Referrer = g.Key!, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topN)
                .ToListAsync();

            return result.Select(x => (x.Referrer, x.Count)).ToList();
        }

    }
}
