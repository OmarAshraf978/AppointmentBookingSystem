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

namespace Application.Features.ServiceProviders.Queries.GetServiceProviderById
{
    public class GetServiceProviderByIdHandler : IRequestHandler<GetServiceProviderByIdQuery, Result<GetServiceProviderByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceProviderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetServiceProviderByIdResponse>> Handle(GetServiceProviderByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(request.Id);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {request.Id} Not Found");
            var services = await _unitOfWork.GetRepository<Service, int>().GetAllBySpecificColumnAsync(x => x.ServiceProviderId == serviceProvider.Id);
            var response = _mapper.Map<GetServiceProviderByIdResponse>(serviceProvider);
            response.AllServices = _mapper.Map<List<ServiceResponse>>(services);
            return response;
        }
    }
}
