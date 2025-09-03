using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Visit
{
    public class VisitEvent
    {
        public int URLTokenId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Referrer { get; set; }
    }
}
