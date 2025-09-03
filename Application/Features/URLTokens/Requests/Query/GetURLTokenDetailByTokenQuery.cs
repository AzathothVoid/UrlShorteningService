using Application.DTOs.URLToken;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.URLTokens.Requests.Query
{
    public class GetURLTokenDetailByTokenQuery : IRequest<CustomQueryResponse<URLTokenDto>>
    {
        public string Token { get; set; } = null!;
    }
}
