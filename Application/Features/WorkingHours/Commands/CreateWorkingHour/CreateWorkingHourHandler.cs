using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Commands.CreateWorkingHour
{
    public class CreateWorkingHourHandler : IRequestHandler<CreateWorkingHourCommand, Result<CreateWorkingHourResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CreateWorkingHourHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<CreateWorkingHourResponse>> Handle(CreateWorkingHourCommand request, CancellationToken cancellationToken)
        {
            if (request.EndTime <= request.StartTime)
                return Error.Failure("End Time Must Be Greater Than Start Time");

            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(request.ServiceProviderId);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {request.ServiceProviderId} Not Found");
            if (serviceProvider.IsDeleted)
                return Error.Failure($"Service Provider With Id : {request.ServiceProviderId} Not Available");
            if (!serviceProvider.IsApproved)
                return Error.Failure($"Service Provider With Id : {request.ServiceProviderId} Not Approved");

            var isServiceProvider = serviceProvider.UserId == _userContext.UserId;
            if (!isServiceProvider)
                return Error.Unauthorized("You Are Not Authorized To Add Working Hours For This Service Provider");

            var anyWorkingHours = await _unitOfWork.GetRepository<WorkingHour, int>()
                                               .AnyAsync(x => x.ServiceProviderId == request.ServiceProviderId
                                                      && x.DayOfWeek == request.DayOfWeek.ToString()
                                                      && request.StartTime < x.EndTime
                                                      && request.EndTime > x.StartTime);
            if (anyWorkingHours)
                return Error.Failure("This Working Hour Overlaps With An Existing Working Hour");

            var workingHour = _mapper.Map<WorkingHour>(request);
            await _unitOfWork.GetRepository<WorkingHour, int>().AddAsync(workingHour);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");

            return _mapper.Map<CreateWorkingHourResponse>(workingHour);
        }
    }
}
