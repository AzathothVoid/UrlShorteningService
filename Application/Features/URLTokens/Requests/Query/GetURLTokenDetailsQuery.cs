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
    public class GetURLTokenDetailsQuery : IRequest<CustomQueryResponse<URLTokenDto>>
    {
        public int Id { get; set; }
        }
}
