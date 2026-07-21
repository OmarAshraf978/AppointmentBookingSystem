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

namespace Application.Features.Services.Queries.GetAllServices
{
    #region GetAllServicesHandler
    public class GetAllServicesHandler : IRequestHandler<GetAllServicesQuery, Result<IEnumerable<GetAllServicesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllServicesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetAllServicesResponse>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _unitOfWork.GetRepository<Service, int>().GetAllAsync();
            if (!services.Any())
                return Error.NotFound("There Is No Services");
            var response = _mapper.Map<IEnumerable<GetAllServicesResponse>>(services);
            return Result<IEnumerable<GetAllServicesResponse>>.Ok(response);
        }
    }
    #endregion
}
