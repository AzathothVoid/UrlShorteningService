using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//For now update will NOT update tokens. A token once created cannot be changed. Only a new token can be issued for the URL
namespace Application.DTOs.URLToken
{
    public class UpdateURLTokenDto : IURLTokenDto
    {
        public string OriginalUrl { get; set; } = null!;
        public DateTime? ExpiresAt { get; set; }
    }
}
