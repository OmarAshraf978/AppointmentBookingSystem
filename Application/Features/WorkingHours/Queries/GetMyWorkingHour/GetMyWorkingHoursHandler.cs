using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Queries.GetMyWorkingHour
{
    public class GetMyWorkingHoursHandler : IRequestHandler<GetMyWorkingHoursQuery, Result<IEnumerable<GetMyWorkingHourResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetMyWorkingHoursHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetMyWorkingHourResponse>>> Handle(GetMyWorkingHoursQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetBySpecificColumnAsync(x => x.UserId == userId);
            if (serviceProvider is null)
                return Error.NotFound("This Service Provider Not Found");
            if (!serviceProvider.IsApproved)
                return Error.Failure("This Service Provider Not Approved");
            if (serviceProvider.IsDeleted)
                return Error.Failure("This Service Provider Not Available");

            var workingHours = await _unitOfWork.GetRepository<WorkingHour, int>()
                                                .GetAllBySpecificColumnWithIncludeAsync(x => x.ServiceProviderId == serviceProvider.Id,
                                                                                        x => x.ServiceProvider);
            if (!workingHours.Any())
                return Error.NotFound("There Is No Working Hours For This Service Provider");

            var response = _mapper.Map<IEnumerable<GetMyWorkingHourResponse>>(workingHours);
            return Result<IEnumerable<GetMyWorkingHourResponse>>.Ok(response);                                       
        }
    }
}
