using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Visit
    {
        public long Id { get; set; }
        public int URLTokenId { get; set; }          
        public URLToken? ShortUrl { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Referrer { get; set; }
        public string? Country { get; set; }         
        public string? DeviceType { get; set; }     
        public string? Browser { get; set; }
        public bool IsBot { get; set; } = false;
    }
}
