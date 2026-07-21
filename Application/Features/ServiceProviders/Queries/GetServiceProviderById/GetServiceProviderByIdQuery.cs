using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using Application.Features.Services.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Queries.GetServiceProviderById
{
    #region GetServiceProviderByIdQuery
    public class GetServiceProviderByIdQuery : IRequest<Result<GetServiceProviderByIdResponse>>
    {
        public int Id { get; set; }
        public GetServiceProviderByIdQuery(int id)
        {
            Id = id;
        }
    }
    #endregion
}
