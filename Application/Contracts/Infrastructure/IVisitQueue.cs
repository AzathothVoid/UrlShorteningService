using Application.Models.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IVisitQueue
    {
        public ChannelReader<VisitEvent> Reader { get; }
        ValueTask EnqueueAsync(VisitEvent e);
        public void Dispose();
    }
}
