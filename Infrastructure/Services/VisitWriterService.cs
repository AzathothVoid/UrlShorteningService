using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Infrastructure.Services
{
    public class VisitWriterService : BackgroundService
    {
        private readonly IServiceProvider _sp;
        private readonly VisitQueue _queue;
        private readonly ILogger<VisitWriterService> _logger;
        private readonly IMapper _mapper;

        public VisitWriterService(VisitQueue queue, IServiceProvider sp, ILogger<VisitWriterService> logger, IMapper mapper)
        {
            _queue = queue;
            _sp = sp;
            _logger = logger;
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var batch = new List<Visit>();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var hasItem = await _queue.Reader.WaitToReadAsync(stoppingToken);
                    if (!hasItem) break;

                    while (_queue.Reader.TryRead(out var e))
                    {
                        var visit = _mapper.Map<Visit>(e);
                        batch.Add(visit);

                        if (batch.Count >= 100) break; 
                    }

                    if (batch.Count > 0)
                    {
                        using var scope = _sp.CreateScope();
                        var repo = scope.ServiceProvider.GetRequiredService<IVisitRepository>();
                        await repo.AddBatchAsync(batch, stoppingToken);
                        batch.Clear();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error writing visits");
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}
