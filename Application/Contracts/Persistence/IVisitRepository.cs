using Application.Contracts.Persistence.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task AddBatchAsync(List<Visit> batch, CancellationToken stoppingToken);
    }
}
