using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Queries.GetReviewById
{
    #region GetReviewByIdHandler
    public class GetReviewByIdHandler : IRequestHandler<GetReviewByIdQuery, Result<GetReviewByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetReviewByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Result<GetReviewByIdResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.GetRepository<Review, int>()
                                          .GetBySpecificColumnWithIncludeAsync(x => x.Id == request.Id,
                                                                               x => x.Service,
                                                                               x => x.Service.ServiceProvider);
            if (review is null)
                return Error.NotFound($"Review With Id :{request.Id} Not Found");
            if (review.IsDeleted)
                return Error.NotFound($"Review With Id :{request.Id} Was Deleted");
            if (review.Service.IsDeleted)
                return Error.NotFound($"Service Not Available");
            var isAdmin = _userContext.Role == "Admin";
            var isServiceProvider = review.Service.ServiceProvider.UserId == _userContext.UserId;
            if (!isServiceProvider && !isAdmin)
                return Error.Unauthorized("You Are Not Allowed To View This Review");

            return _mapper.Map<GetReviewByIdResponse>(review);
        }
    }
    #endregion
}
