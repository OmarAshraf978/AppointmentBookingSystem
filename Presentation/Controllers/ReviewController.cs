using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Commands.DeleteReview;
using Application.Features.Reviews.Outputs;
using Application.Features.Reviews.Queries.GetReviewById;
using Application.Features.Reviews.Queries.GetReviewsForService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    #region ReviewController
    public class ReviewController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "User")]
        [HttpPost("CreateReview")]
        public async Task<ActionResult<CreateReviewResponse>> CreateReview(CreateReviewCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpGet("GetReviewsForService/{id}")]
        public async Task<ActionResult<IEnumerable<GetReviewsForServiceResponse>>> GetReviewsForService(int id)
        {
            var Result = await _mediator.Send(new GetReviewsForServiceQuery(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpGet("GetReviewById/{id}")]
        public async Task<ActionResult<GetReviewByIdResponse>> GetReviewById(int id)
        {
            var Result = await _mediator.Send(new GetReviewByIdQuery(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, Service Provider, User")]
        [HttpPut("DeleteReview/{id}")]
        public async Task<ActionResult<DeleteReviewResponse>> DeleteReview(int id)
        {
            var Result = await _mediator.Send(new DeleteReviewCommand(id));
            return HandleResult(Result);
        }
    }
    #endregion
}
