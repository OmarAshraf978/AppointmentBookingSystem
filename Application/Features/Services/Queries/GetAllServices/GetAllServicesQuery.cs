using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Queries.GetAllServices
{
    public class GetAllServicesQuery : IRequest<Result<IEnumerable<GetAllServicesResponse>>>
    {
    }
}
