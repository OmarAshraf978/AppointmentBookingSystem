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

namespace Application.Features.Services.Queries.GetServiceById
{
    #region GetServiceByIdHandler
    public class GetServiceByIdHandler : IRequestHandler<GetServiceByIdQuery, Result<GetServiceByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetServiceByIdResponse>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.GetRepository<Service, int>().GetByIdAsync(request.Id);
            if (service is null)
                return Error.NotFound($"Service With Id : {request.Id} Not Found");
            return _mapper.Map<GetServiceByIdResponse>(service);
        }
    }
    #endregion
}
