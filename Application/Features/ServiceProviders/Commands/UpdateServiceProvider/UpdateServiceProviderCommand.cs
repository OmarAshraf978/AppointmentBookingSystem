using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Commands.UpdateServiceProvider
{
    #region UpdateServiceProviderCommand
    public class UpdateServiceProviderCommand : IRequest<Result<UpdateServiceProviderResponse>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string BusinessName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    #endregion
}
