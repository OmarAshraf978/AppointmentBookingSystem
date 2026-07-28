using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Queries.GetWorkingHoursByServiceProviderId
{
    public class GetWorkingHoursByServiceProviderIdQuery : IRequest<Result<IEnumerable<GetWorkingHoursByServiceProviderIdResponse>>>
    {
        public int Id { get; set; }
        public GetWorkingHoursByServiceProviderIdQuery(int id)
        {
            Id = id;
        }
    }
}
