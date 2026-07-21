using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using Domain.Entities.Modules;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Commands.CreateReview
{
    #region CreateReviewCommand
    public class CreateReviewCommand : IRequest<Result<CreateReviewResponse>>
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public int ServiceId { get; set; }
    }
    #endregion
}
