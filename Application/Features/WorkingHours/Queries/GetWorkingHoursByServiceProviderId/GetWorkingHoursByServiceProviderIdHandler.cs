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

namespace Application.Features.WorkingHours.Queries.GetWorkingHoursByServiceProviderId
{
    #region GetWorkingHoursByServiceProviderIdHandler
    public class GetWorkingHoursByServiceProviderIdHandler : IRequestHandler<GetWorkingHoursByServiceProviderIdQuery, Result<IEnumerable<GetWorkingHoursByServiceProviderIdResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWorkingHoursByServiceProviderIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetWorkingHoursByServiceProviderIdResponse>>> Handle(GetWorkingHoursByServiceProviderIdQuery request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(request.Id);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {request.Id} Not Found");
            if (serviceProvider.IsDeleted)
                return Error.Failure($"Service Provider With Id : {request.Id} Not Available");
            if (!serviceProvider.IsApproved)
                return Error.Failure($"Service Provider With Id : {request.Id} Not Approved");

            var WorkingHours = await _unitOfWork.GetRepository<WorkingHour, int>()
                                                .GetAllBySpecificColumnWithIncludeAsync(x => x.ServiceProviderId == request.Id,
                                                                                        x => x.ServiceProvider);
            if (!WorkingHours.Any())
                return Error.NotFound($"There Is No Working Hours For Service Provider : {request.Id}");

            var response = _mapper.Map<IEnumerable<GetWorkingHoursByServiceProviderIdResponse>>(WorkingHours);
            return Result<IEnumerable<GetWorkingHoursByServiceProviderIdResponse>>.Ok(response);
        }
    }
    #endregion
}
