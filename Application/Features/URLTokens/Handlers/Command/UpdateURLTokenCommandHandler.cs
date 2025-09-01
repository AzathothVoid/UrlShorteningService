using Application.Contracts.Persistence;
using Application.DTOs.URLToken.Validators;
using Application.Features.URLTokens.Requests.Command;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.URLTokens.Handlers.Command
{
    public class UpdateURLTokenCommandHandler :IRequestHandler<UpdateURLTokenCommand, CustomCommandResponse> 
    {
        private readonly IURLTokenRepository _urlTokenRepository;
        private readonly IMapper _mapper;

        public UpdateURLTokenCommandHandler(IURLTokenRepository urlTokenRepository, IMapper mapper
            )
        {
            _urlTokenRepository = urlTokenRepository;
            _mapper = mapper;
        }

        public async Task<CustomCommandResponse> Handle(UpdateURLTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new CustomCommandResponse();
            var validator = new UpdateURLTokenDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UrlTokenDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update failed";
                response.Errors = JsonConvert.SerializeObject(validationResult.Errors.Select(q => q.ErrorMessage).ToList());
                return response;
            }

            var urlToken = await _urlTokenRepository.GetByIdAsync(request.Id);

            if (urlToken == null)
            {
                response.Success = false;
                response.Message = "Update failed";
                response.Errors = "URL Token not found";
                return response;
            }

            _mapper.Map(request.UrlTokenDto, urlToken);

            await _urlTokenRepository.UpdateAsync(urlToken);

            response.Success = true;
            response.Message = "Update Successful";
            return response;
        }
    }
}
