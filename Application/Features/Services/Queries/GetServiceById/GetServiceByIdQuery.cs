using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Queries.GetServiceById
{
    #region GetServiceByIdQuery
    public class GetServiceByIdQuery : IRequest<Result<GetServiceByIdResponse>>
    {
        public int Id { get; set; }
        public GetServiceByIdQuery(int id)
        {
            Id = id;
        }
    }
    #endregion
}
