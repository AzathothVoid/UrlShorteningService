using Application.Contracts.Infrastructure;
using Application.Models.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class VisitQueue : IVisitQueue, IDisposable
    {
        private readonly Channel<VisitEvent> _channel = Channel.CreateUnbounded<VisitEvent>();
        public ValueTask EnqueueAsync(VisitEvent e) => _channel.Writer.WriteAsync(e);
        public ChannelReader<VisitEvent> Reader => _channel.Reader;
        public void Dispose() => _channel.Writer.Complete();
    }
}
