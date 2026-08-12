using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Commands.DeleteReview
{
    #region DeleteReviewHandler
    public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, Result<DeleteReviewResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public DeleteReviewHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }
        public async Task<Result<DeleteReviewResponse>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.GetRepository<Review, int>()
                                          .GetBySpecificColumnWithIncludeAsync(x => x.Id == request.Id,
                                                                               x => x.Service,
                                                                               x => x.Service.ServiceProvider);
            if (review is null)
                return Error.NotFound($"Review With Id :{request.Id} Not Found");
            if (review.IsDeleted)
                return Error.NotFound("This Review Already Deleted");
            if (review.Service.IsDeleted)
                return Error.NotFound("This Service Already Deleted");

            var isAdmin = _userContext.Role == "Admin";
            var isServiceProvider = review.Service.ServiceProvider.UserId == _userContext.UserId;
            var isUser = review.UserId == _userContext.UserId;
            if (!isAdmin && !isServiceProvider && !isUser)
                return Error.Unauthorized("You Are Not Allowed To Delete Review");

            review.IsDeleted = true;
            _unitOfWork.GetRepository<Review, int>().Update(review);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");
            return new DeleteReviewResponse { Message = "Review deleted Successfully" };
        }
    }
    #endregion
}
