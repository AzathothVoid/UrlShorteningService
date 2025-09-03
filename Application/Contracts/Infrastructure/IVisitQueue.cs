using Application.Models.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IVisitQueue
    {
        ValueTask EnqueueAsync(VisitEvent e);
    }
}
