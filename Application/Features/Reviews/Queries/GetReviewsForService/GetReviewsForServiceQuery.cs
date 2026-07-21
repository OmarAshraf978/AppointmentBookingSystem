using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Queries.GetReviewsForService
{
    public class GetReviewsForServiceQuery : IRequest<Result<IEnumerable<GetReviewsForServiceResponse>>>
    {
        public int ServiceId { get; set; }
        public GetReviewsForServiceQuery(int id)
        {
            ServiceId = id;
        }
    }
}
