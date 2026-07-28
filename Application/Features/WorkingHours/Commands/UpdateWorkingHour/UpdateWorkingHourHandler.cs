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
using Microsoft.EntityFrameworkCore.Metadata;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Commands.UpdateWorkingHour
{
    #region UpdateWorkingHourHandler
    public class UpdateWorkingHourHandler : IRequestHandler<UpdateWorkingHourCommand, Result<UpdateWorkingHourResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public UpdateWorkingHourHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<UpdateWorkingHourResponse>> Handle(UpdateWorkingHourCommand request, CancellationToken cancellationToken)
        {
            var workingHour = await _unitOfWork.GetRepository<WorkingHour, int>()
                                               .GetBySpecificColumnWithIncludeAsync(x => x.Id == request.Id,
                                                                                    x => x.ServiceProvider);
            if (workingHour is null)
                return Error.NotFound($"Working Hour With Id : {request.Id} Not Found");

            var isServiceProvider = workingHour.ServiceProvider.UserId == _userContext.UserId;
            if (!isServiceProvider)
                return Error.Unauthorized("You Are Not Authorized To Update This Working Hour");

            var serviceProviderId = workingHour.ServiceProviderId;

            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(serviceProviderId);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {serviceProviderId} Not Found");
            if (serviceProvider.IsDeleted)
                return Error.Failure($"Service Provider With Id : {serviceProviderId} Not Available");
            if (!serviceProvider.IsApproved)
                return Error.Failure($"Service Provider With Id : {serviceProviderId} Not Approved");

            var anyWorkingHours = await _unitOfWork.GetRepository<WorkingHour, int>()
                                                  .AnyAsync(x => x.ServiceProviderId == serviceProviderId
                                                         && x.DayOfWeek == request.DayOfWeek.ToString()
                                                         && request.StartTime < x.EndTime
                                                         && request.EndTime > x.StartTime
                                                         && x.Id != request.Id);
            if (anyWorkingHours)
                return Error.Failure("This Working Hour Overlaps With An Existing Working Hour");
         
            _mapper.Map(request, workingHour);
            _unitOfWork.GetRepository<WorkingHour, int>().Update(workingHour);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");

            return _mapper.Map<UpdateWorkingHourResponse>(workingHour);
        }
    }
    #endregion
}
