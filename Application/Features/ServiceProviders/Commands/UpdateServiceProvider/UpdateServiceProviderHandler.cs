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
using MediatR.Pipeline;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Commands.UpdateServiceProvider
{
    public class UpdateServiceProviderHandler : IRequestHandler<UpdateServiceProviderCommand, Result<UpdateServiceProviderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateServiceProviderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<UpdateServiceProviderResponse>> Handle(UpdateServiceProviderCommand request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(request.Id);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {request.Id} Not Found");
            _mapper.Map(request, serviceProvider);
            _unitOfWork.GetRepository<ServiceProvider, int>().Update(serviceProvider);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");
            return _mapper.Map<UpdateServiceProviderResponse>(serviceProvider);
        }
    }
}
