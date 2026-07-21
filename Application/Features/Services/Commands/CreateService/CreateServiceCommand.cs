using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Commands.CreateService
{
    #region CreateServiceCommand
    public class CreateServiceCommand : IRequest<Result<CreateServiceResponse>>
    {
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int DurationInMinutes { get; set; }
        public int ServiceProviderId { get; set; }
    }
    #endregion
}
