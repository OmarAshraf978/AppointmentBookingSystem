using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Result<CreateReviewResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CreateReviewHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<CreateReviewResponse>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.GetRepository<Service, int>().GetByIdAsync(request.ServiceId);
            if (service is null)
                return Error.NotFound($"Service With Id : {request.ServiceId} Not Found");

            if (service.IsDeleted)
                return Error.NotFound("This Service Is Not Available");

            var appointment = await _unitOfWork.GetRepository<Appointment, int>()
                                               .GetBySpecificColumnAsync(x => x.UserId == _userContext.UserId
                                                                         && x.ServiceId == request.ServiceId
                                                                         && x.Status == AppointmentStatus.Approved);
            if (appointment is null)
                return Error.Failure("You Can Only Review Services With An Approved Appointment");

            var review = _mapper.Map<Review>(request);
            review.CreatedAt = DateTime.UtcNow;
            review.UserId = _userContext.UserId;

            await _unitOfWork.GetRepository<Review, int>().AddAsync(review);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");

            return _mapper.Map<CreateReviewResponse>(review);
        }
    }
}
