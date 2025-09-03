using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.URLToken.Validators
{
    public class UpdateURLTokenDtoValidator : AbstractValidator<UpdateURLTokenDto>
    {
        public UpdateURLTokenDtoValidator()
        {
            Include(new IURLTokenDtoValidator());

            RuleFor(x => x.Clicks)
                .GreaterThanOrEqualTo(0).WithMessage("Clicks must be greater than or equal to 0.");

        }
    }
}
