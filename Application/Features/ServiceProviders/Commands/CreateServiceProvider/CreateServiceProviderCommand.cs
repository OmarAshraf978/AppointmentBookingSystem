using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Commands.CreateServiceProvider
{
    #region CreateServiceProviderCommand
    public class CreateServiceProviderCommand : IRequest<Result<CreateServiceProviderResponse>>
    {
        public string BusinessName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    #endregion
}
