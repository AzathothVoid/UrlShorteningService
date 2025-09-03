using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task AddBatchAsync(List<Visit> batch, CancellationToken stoppingToken);
        Task<long> GetTotalVisitsAsync(int? URLTokenId);
        Task<long> GetUniqueVisitorsByIpAsync(int? URLTokenId);
        Task<List<(DateTimeOffset date, int count)>> GetDailyCountsAsync(int? URLTokenId, int days = 30);
        Task<List<(string Referrer, int Count)>> GetTopReferrersAsync(int? URLTokenId, int topN = 10);
        Task<List<(string label, int count)>> GetDeviceBreakdownAsync(int? URLTokenId = null);
        Task<List<(string label, int count)>> GetBrowserBreakdownAsync(int? URLTokenId = null);
        Task<List<(DateTime timestamp, string ip, string ua, string referrer)>> GetRecentVisitsAsync(int? URLTokenId = null, int limit = 20);
        Task<List<(int id, string token)>> ListURLTokensAsync(int limit = 50); 
    }
}
