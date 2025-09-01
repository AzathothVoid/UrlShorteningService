using Application.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses
{
    public class CustomQueryResponse<T> : BaseResponse where T : class
    {
        public T? Data { get; set; }
    }
}
