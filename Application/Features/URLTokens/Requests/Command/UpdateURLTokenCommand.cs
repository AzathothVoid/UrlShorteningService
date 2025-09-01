using Application.DTOs.URLToken;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.URLTokens.Requests.Command
{
    public class UpdateURLTokenCommand : IRequest<CustomCommandResponse>
    {
        public int Id { get; set; }
        public UpdateURLTokenDto UrlTokenDto { get; set; }
    }
}
