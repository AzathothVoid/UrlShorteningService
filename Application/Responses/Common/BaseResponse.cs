using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses.Common
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Errors { get; set; }
    }
}
