using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Commands.DeleteReview
{
    #region DeleteReviewCommand
    public class DeleteReviewCommand : IRequest<Result<DeleteReviewResponse>>
    {
        public int Id { get; set; }
        public DeleteReviewCommand(int id)
        {
            Id = id;
        }
    }
    #endregion
}
