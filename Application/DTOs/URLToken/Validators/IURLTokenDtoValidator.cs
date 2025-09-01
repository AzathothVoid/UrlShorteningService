using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.URLToken.Validators
{
    public class IURLTokenDtoValidator : AbstractValidator<IURLTokenDto>
    {
        public IURLTokenDtoValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.OriginalUrl).NotEmpty().WithMessage("Original URL cannot be empty.")
                                      .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                                      .WithMessage("Original URL must be a valid absolute URL.");
            RuleFor(x => x.ExpiresAt).Must(date => date == null || date > DateTime.UtcNow)
                                     .WithMessage("Expiration date must be in the future if provided.");
        }
    }
}
