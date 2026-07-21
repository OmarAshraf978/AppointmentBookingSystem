using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Queries.GetReviewById
{
    #region GetReviewByIdQuery
    public class GetReviewByIdQuery : IRequest<Result<GetReviewByIdResponse>>
    {
        public int Id { get; set; }
        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }
    }
    #endregion
}
