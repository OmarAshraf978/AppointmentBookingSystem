using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Commands.DeleteServiceProvider
{
    public class DeleteServiceProviderCommand : IRequest<Result<DeleteServiceProviderResponse>>
    {
        public int Id { get; set; }
        public DeleteServiceProviderCommand(int id)
        {
            Id = id;
        }
    }
}
