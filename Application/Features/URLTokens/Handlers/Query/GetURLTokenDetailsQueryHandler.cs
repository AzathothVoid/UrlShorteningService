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
    public class GetURLTokenDetailsQueryHandler : IRequestHandler<GetURLTokenDetailsQuery, CustomQueryResponse<URLTokenDto>>
    {
        private readonly IURLTokenRepository _urlTokenRepository;
        private readonly IMapper _mapper;

        public GetURLTokenDetailsQueryHandler(IURLTokenRepository urlTokenRepository, IMapper mapper
            )
        {
            _urlTokenRepository = urlTokenRepository;
            _mapper = mapper;
        }
        public async Task<CustomQueryResponse<URLTokenDto>> Handle(GetURLTokenDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new CustomQueryResponse<URLTokenDto>();
            var urlTokenDetail = await _urlTokenRepository.GetByIdAsync(request.Id);

            response.Success = true;
            response.Message = "GET Successful";
            response.Data = _mapper.Map<URLTokenDto>(urlTokenDetail);

            return response;
        }
    }
}
