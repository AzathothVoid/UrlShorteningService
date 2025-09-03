using Application.Contracts.Persistence;
using Application.DTOs.URLToken.Validators;
using Application.Features.URLTokens.Requests.Command;
using Application.Responses;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.URLTokens.Handlers.Command
{
    public class UpdateClickURLTokenCommandHandler : IRequestHandler<UpdateClickURLTokenCommand, CustomCommandResponse>
    {
        private readonly IURLTokenRepository _urlTokenRepository;
        private readonly IMapper _mapper;

        public UpdateClickURLTokenCommandHandler(IURLTokenRepository urlTokenRepository, IMapper mapper
            )
        {
            _urlTokenRepository = urlTokenRepository;
            _mapper = mapper;
        }

        public async Task<CustomCommandResponse> Handle(UpdateClickURLTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new CustomCommandResponse();

            var urlToken = await _urlTokenRepository.GetByTokenAsync(request.Token);

            if (urlToken == null)
            {
                response.Success = false;
                response.Message = "Click Update failed";
                response.Errors = "URL Token not found";
                return response;
            }

            await _urlTokenRepository.UpdateTokenClick(urlToken);

            response.Success = true;
            response.Message = "Update Successful";
            return response;
        }
    }
}
