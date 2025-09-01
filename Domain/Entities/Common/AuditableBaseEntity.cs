using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class AuditableBaseEntity : BaseEntity, IAuditableBaseEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
