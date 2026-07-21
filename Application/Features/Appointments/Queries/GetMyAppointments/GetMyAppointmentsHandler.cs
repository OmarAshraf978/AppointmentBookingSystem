using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using Application.Features.Services.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Queries.GetMyAppointments
{
    #region GetMyAppointmentsHandler
    public class GetMyAppointmentsHandler : IRequestHandler<GetMyAppointmentsQuery, Result<IEnumerable<GetMyAppointmentsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetMyAppointmentsHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetMyAppointmentsResponse>>> Handle(GetMyAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _unitOfWork.GetRepository<Appointment, int>()
                                                .GetAllBySpecificColumnWithIncludeAsync(x => x.UserId == _userContext.UserId,
                                                                                        x => x.Service,
                                                                                        x => x.Service.ServiceProvider);
                                                                        
            if (!appointments.Any())
                return Error.NotFound("There Is No Appointments.");

            var response = _mapper.Map<IEnumerable<GetMyAppointmentsResponse>>(appointments);
            return Result<IEnumerable<GetMyAppointmentsResponse>>.Ok(response);
        }
    }
    #endregion
}
