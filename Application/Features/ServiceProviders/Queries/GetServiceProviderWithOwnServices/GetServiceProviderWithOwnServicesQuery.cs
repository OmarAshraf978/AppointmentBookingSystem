using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Queries.GetServiceProviderWithOwnServices
{
    #region GetServiceProviderWithOwnServicesQuery
    public class GetServiceProviderWithOwnServicesQuery : IRequest<Result<GetServiceProviderWithOwnServicesResponse>>
    {
    }
    #endregion
}
