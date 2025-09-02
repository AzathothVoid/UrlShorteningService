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
    public class CreateURLTokenCommandHandler : IRequestHandler<CreateURLTokenCommand, CustomCommandResponse>
    {
        private readonly IURLTokenRepository _urlTokenRepository;
        private readonly IMapper _mapper;

        public CreateURLTokenCommandHandler(IURLTokenRepository urlTokenRepository, IMapper mapper
            )
        {
            _urlTokenRepository = urlTokenRepository;
            _mapper = mapper;
        }

        public async Task<CustomCommandResponse> Handle(CreateURLTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new CustomCommandResponse();
            var validator = new CreateURLTokenDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UrlTokenDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = JsonConvert.SerializeObject(validationResult.Errors.Select(q => q.ErrorMessage).ToList());
                return response;
            }

            var urlToken = _mapper.Map<URLToken>(request.UrlTokenDto);

            urlToken = await _urlTokenRepository.AddAsync(urlToken);

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = urlToken.Id;
            return response;
        }
    }
}
