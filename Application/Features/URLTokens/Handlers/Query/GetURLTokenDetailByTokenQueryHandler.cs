using Application.Contracts.Persistence;
using Application.DTOs.URLToken;
using Application.Features.URLTokens.Requests.Query;
using Application.Responses;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.URLTokens.Handlers.Query
{
    public class GetURLTokenDetailByTokenQueryHandler : IRequestHandler<GetURLTokenDetailByTokenQuery, CustomQueryResponse<URLTokenDto>>
    {
        private readonly IURLTokenRepository _urlTokenRepository;
        private readonly IMapper _mapper;

        public GetURLTokenDetailByTokenQueryHandler(IURLTokenRepository urlTokenRepository, IMapper mapper
            )
        {
            _urlTokenRepository = urlTokenRepository;
            _mapper = mapper;
        }
        public async Task<CustomQueryResponse<URLTokenDto>> Handle(GetURLTokenDetailByTokenQuery request, CancellationToken cancellationToken)
        {
            var response = new CustomQueryResponse<URLTokenDto>();
            var urlTokenDetail = await _urlTokenRepository.GetByTokenAsync(request.Token);
            
            if (urlTokenDetail == null)
            {
                response.Success = false;
                response.Message = "Token not found";
                return response;
            }
            response.Success = true;
            response.Message = "GET Successful";
            response.Data = _mapper.Map<URLTokenDto>(urlTokenDetail);

            return response;
        }
    }
}
