using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Queries.GetServiceProviderWithOwnServices
{
    public class GetServiceProviderWithOwnServicesHandler : IRequestHandler<GetServiceProviderWithOwnServicesQuery, Result<GetServiceProviderWithOwnServicesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetServiceProviderWithOwnServicesHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<GetServiceProviderWithOwnServicesResponse>> Handle(GetServiceProviderWithOwnServicesQuery request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetBySpecificColumnAsync(x => x.UserId == _userContext.UserId);
            if (serviceProvider is null)
                return Error.NotFound("Service Provider Not Found");
            var services = await _unitOfWork.GetRepository<Service, int>().GetAllBySpecificColumnAsync(x => x.ServiceProviderId == serviceProvider.Id & x.IsDeleted == false);
            var response = _mapper.Map<GetServiceProviderWithOwnServicesResponse>(serviceProvider);
            response.AllServices = _mapper.Map<List<ServiceResponse>>(services);
            return response;
        }
    }
}
