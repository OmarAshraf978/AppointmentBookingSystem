using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Services.Commands.CreateService
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.ServiceName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.DurationInMinutes).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ServiceProviderId).NotEmpty().GreaterThan(0);
        }
    }
}
