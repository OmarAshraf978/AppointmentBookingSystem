using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Queries.GetAppointmentsForServiceProvider
{
    #region GetAppointmentsForServiceProviderHandler
    public class GetAppointmentsForServiceProviderHandler : IRequestHandler<GetAppointmentsForServiceProviderQuery, Result<IEnumerable<GetAppointmentsForServiceProviderResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetAppointmentsForServiceProviderHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetAppointmentsForServiceProviderResponse>>> Handle(GetAppointmentsForServiceProviderQuery request, CancellationToken cancellationToken)
        {
            var appointmentsForServiceProvider = await _unitOfWork.GetRepository<Appointment, int>()
                                                .GetAllBySpecificColumnWithIncludeAsync(x => x.Service.ServiceProvider.UserId == _userContext.UserId,
                                                                                        x => x.Service,
                                                                                        x => x.Service.ServiceProvider);

            if (!appointmentsForServiceProvider.Any())
                return Error.NotFound("There Is No Appointments.");

            var response = _mapper.Map<IEnumerable<GetAppointmentsForServiceProviderResponse>>(appointmentsForServiceProvider);
            return Result<IEnumerable<GetAppointmentsForServiceProviderResponse>>.Ok(response);
        }
    }
    #endregion
}
