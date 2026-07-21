using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Outputs;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.ServiceProviders.Commands.ApproveServiceProvider
{
    public class ApproveServiceProviderHandler : IRequestHandler<ApproveServiceProviderCommand, Result<ApproveServiceProviderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveServiceProviderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ApproveServiceProviderResponse>> Handle(ApproveServiceProviderCommand request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(request.Id);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {request.Id} Not Found");
            if (serviceProvider.IsApproved)
                return Error.Failure("This Service Provider is already approved.");
            serviceProvider.IsApproved = true;
            _unitOfWork.GetRepository<ServiceProvider, int>().Update(serviceProvider);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");
            return new ApproveServiceProviderResponse { Message = "Service Provider Approved Successfully" };
        }
    }
}
