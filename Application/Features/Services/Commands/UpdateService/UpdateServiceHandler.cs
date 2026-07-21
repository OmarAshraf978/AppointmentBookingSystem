using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Commands.UpdateService
{
    public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, Result<UpdateServiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<UpdateServiceResponse>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.GetRepository<Service, int>().GetByIdAsync(request.Id);
            if (service is null)
                return Error.NotFound($"Service With Id : {request.Id} Not Found");
            _mapper.Map(request, service);
            _unitOfWork.GetRepository<Service, int>().Update(service);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("SomeThing Went Wrong");
            return _mapper.Map<UpdateServiceResponse>(service);
        }
    }
}
