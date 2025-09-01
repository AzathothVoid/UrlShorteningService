using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.URLToken
{
    public class CreateURLTokenDto : IURLTokenDto
    {
        public string Token { get; set; } = null!;
        public string OriginalUrl { get; set; } = null!;
        public DateTime? ExpiresAt { get; set; }
    }
}
