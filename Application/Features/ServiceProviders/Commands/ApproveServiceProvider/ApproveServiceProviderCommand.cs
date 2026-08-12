using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Commands.ApproveServiceProvider
{
    #region ApproveServiceProviderCommand
    public class ApproveServiceProviderCommand : IRequest<Result<ApproveServiceProviderResponse>>
    {
        public int Id { get; set; }
        public ApproveServiceProviderCommand(int id)
        {
            Id = id;
        }
    }
    #endregion
}
