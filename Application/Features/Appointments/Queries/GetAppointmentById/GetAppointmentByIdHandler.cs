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

namespace Application.Features.Appointments.Queries.GetAppointmentById
{
    #region GetAppointmentByIdHandler
    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, Result<GetAppointmentByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public GetAppointmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Result<GetAppointmentByIdResponse>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.GetRepository<Appointment, int>()
                                               .GetBySpecificColumnWithIncludeAsync(x => x.Id == request.Id,
                                                                                    x => x.Service,
                                                                                    x => x.Service.ServiceProvider);
            if (appointment is null)
                return Error.NotFound($"Appointment With Id : {request.Id} Not Found");

            var isAdmin = _userContext.Role == "Admin";
            var isUser = appointment.UserId == _userContext.UserId;
            var isServiceProvider = appointment.Service.ServiceProvider.UserId == _userContext.UserId;

            if (!isAdmin &&  !isUser && !isServiceProvider)
                return Error.Unauthorized("You are not authorized to view this appointment.");

            return _mapper.Map<GetAppointmentByIdResponse>(appointment);
        }
    }
    #endregion
}
