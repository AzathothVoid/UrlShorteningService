using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.URLToken
{
    public interface IURLTokenDto
    {
        public string OriginalUrl { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
