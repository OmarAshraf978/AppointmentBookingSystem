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

namespace Application.Features.Reviews.Queries.GetReviewsForService
{
    #region GetReviewsForServiceHandler
    public class GetReviewsForServiceHandler : IRequestHandler<GetReviewsForServiceQuery, Result<IEnumerable<GetReviewsForServiceResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetReviewsForServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Result<IEnumerable<GetReviewsForServiceResponse>>> Handle(GetReviewsForServiceQuery request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.GetRepository<Service, int>().GetByIdAsync(request.ServiceId);
            if (service is null)
                return Error.NotFound($"Service With Id : {request.ServiceId} Not Found");
            if (service.IsDeleted)
                return Error.NotFound($"Service With Id : {request.ServiceId} Was Deleted");

            var availableReviews = await _unitOfWork.GetRepository<Review, int>()
                                          .GetAllBySpecificColumnWithIncludeAsync(x => x.ServiceId == request.ServiceId
                                                                                  && !x.IsDeleted,
                                                                                  x => x.Service,
                                                                                  x => x.Service.ServiceProvider);
            if (!availableReviews.Any())
                return Error.NotFound($"There Is No Reviews For Service Id : {request.ServiceId}");

            var isAdmin = _userContext.Role == "Admin";
            var isServiceProvider = availableReviews.First().Service.ServiceProvider.UserId == _userContext.UserId;
            if (!isAdmin && !isServiceProvider)
                return Error.Unauthorized("You Are Not Authorized To View Reviews");

            return _mapper.Map<List<GetReviewsForServiceResponse>>(availableReviews);
        }
    }
    #endregion
}
