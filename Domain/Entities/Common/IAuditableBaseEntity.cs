using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public interface IAuditableBaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
