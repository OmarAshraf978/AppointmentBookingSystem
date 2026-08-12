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

namespace Application.Features.ServiceProviders.Commands.DeleteServiceProvider
{
    #region DeleteServiceProviderHandler
    public class DeleteServiceProviderHandler : IRequestHandler<DeleteServiceProviderCommand, Result<DeleteServiceProviderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceProviderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<DeleteServiceProviderResponse>> Handle(DeleteServiceProviderCommand request, CancellationToken cancellationToken)
        {
            var serviceProvider = await _unitOfWork.GetRepository<ServiceProvider, int>().GetByIdAsync(request.Id);
            if (serviceProvider is null)
                return Error.NotFound($"Service Provider With Id : {request.Id} Not Found");
            if (serviceProvider.IsDeleted)
                return new DeleteServiceProviderResponse { Message = "This Service Provider Is Already Deleted" };
            serviceProvider.IsDeleted = true;
            _unitOfWork.GetRepository<ServiceProvider, int>().Update(serviceProvider);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");
            return new DeleteServiceProviderResponse { Message = "This Service Provider Is Deleted Successfully" };
        }
    }
    #endregion
}
