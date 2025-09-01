using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.URLToken
{
    public class URLTokenDto : BaseDto, IURLTokenDto
    {
        public string Token { get; set; } = null!;
        public string OriginalUrl { get; set; } = null!;
        public int Clicks { get; set; } = 0;
        public DateTime? ExpiresAt { get; set; }
    }
}
