using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class URlToken : AuditableBaseEntity
    {
        public string Token { get; set; } = null!; 
        public string OriginalUrl { get; set; } = null!;
        public int Clicks { get; set; } = 0;
        public DateTime? ExpiresAt { get; set; }
    }
}
