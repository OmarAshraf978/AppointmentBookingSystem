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

namespace Application.Features.Services.Commands.CreateService
{
    #region CreateServiceHandler
    public class CreateServiceHandler : IRequestHandler<CreateServiceCommand, Result<CreateServiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<CreateServiceResponse>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = _mapper.Map<Service>(request);
            var ServiceRepo = _unitOfWork.GetRepository<Service, int>();
            await ServiceRepo.AddAsync(service);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("SomeThing Went Wrong");
            var serviceResponse = _mapper.Map<CreateServiceResponse>(service);
            return serviceResponse;
        }
    }
    #endregion
}
