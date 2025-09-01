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
        }
    }
}
