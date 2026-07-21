using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.ServiceProviders.Commands.CreateServiceProvider
{
    #region CreateServiceProviderValidator
    public class CreateServiceProviderValidator : AbstractValidator<CreateServiceProviderCommand>
    {
        public CreateServiceProviderValidator()
        {
            RuleFor(x => x.BusinessName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(150);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
        }
    }
    #endregion
}
