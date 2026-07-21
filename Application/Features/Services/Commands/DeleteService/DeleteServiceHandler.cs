using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Commands.DeleteService
{
    #region DeleteServiceHandler
    public class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand, Result<DeleteServiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<DeleteServiceResponse>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.GetRepository<Service, int>().GetByIdAsync(request.Id);
            if (service is null)
                return Error.NotFound($"Service With Id : {request.Id} Not Found");
            if (service.IsDeleted == true)
                return new DeleteServiceResponse { Message = "Service Already Deleted" };
            service.IsDeleted = true;
            _unitOfWork.GetRepository<Service, int>().Update(service);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something went Wrong");
            return new DeleteServiceResponse
            {
                Message = "Service Deleted Successfully"
            };
        }
    }
    #endregion
}
