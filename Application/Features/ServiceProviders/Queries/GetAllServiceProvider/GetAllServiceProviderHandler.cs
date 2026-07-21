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

namespace Application.Features.ServiceProviders.Queries.GetAllServiceProvider
{
    public class GetAllServiceProviderHandler : IRequestHandler<GetAllServiceProvidersQuery, Result<IEnumerable<GetAllServiceProvidersResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllServiceProviderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetAllServiceProvidersResponse>>> Handle(GetAllServiceProvidersQuery request, CancellationToken cancellationToken)
        {
            var serviceProviders = await _unitOfWork.GetRepository<ServiceProvider, int>().GetAllWithIncludeAsync(x => x.Services); ;
            if (!serviceProviders.Any())
                return Error.NotFound("There Is No Service Providers");
            var response = _mapper.Map<List<GetAllServiceProvidersResponse>>(serviceProviders);
            return response;
        }
    }
}
