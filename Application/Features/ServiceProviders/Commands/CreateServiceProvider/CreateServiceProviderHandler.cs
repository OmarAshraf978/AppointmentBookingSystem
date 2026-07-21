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

namespace Application.Features.ServiceProviders.Commands.CreateServiceProvider
{
    public class CreateServiceProviderHandler : IRequestHandler<CreateServiceProviderCommand, Result<CreateServiceProviderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CreateServiceProviderHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<CreateServiceProviderResponse>> Handle(CreateServiceProviderCommand request, CancellationToken cancellationToken)
        {
            var serviceProvider = _mapper.Map<ServiceProvider>(request);
            serviceProvider.UserId = _userContext.UserId;
            var ProviderExists = await _unitOfWork.GetRepository<ServiceProvider, int>().AnyAsync(x => x.UserId == _userContext.UserId);
            if (ProviderExists)
                return Error.Failure("You already have a service provider profile");
            await _unitOfWork.GetRepository<ServiceProvider, int>().AddAsync(serviceProvider);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");
            return _mapper.Map<CreateServiceProviderResponse>(serviceProvider);
        }
    }
}
