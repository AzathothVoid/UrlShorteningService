using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.URLToken.Validators
{
    public class CreateURLTokenDtoValidator : AbstractValidator<CreateURLTokenDto>
    {
        public CreateURLTokenDtoValidator()
        {
            Include(new IURLTokenDtoValidator());

            //Can add rule to check if token is unique in the database if needed

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.")
                .MaximumLength(128).WithMessage("Token must not exceed 128 characters.");
        }
    }
}
