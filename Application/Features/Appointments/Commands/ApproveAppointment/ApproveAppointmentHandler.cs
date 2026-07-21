using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using Application.Features.ServiceProviders.Outputs;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Commands.ApproveAppointment
{
    #region ApproveAppointmentHandler
    public class ApproveAppointmentHandler : IRequestHandler<ApproveAppointmentCommand, Result<StateResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveAppointmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<StateResponse>> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.GetRepository<Appointment, int>().GetByIdAsync(request.Id);
            if (appointment is null)
                return Error.NotFound($"Appointment With Id : {request.Id} Not Found");
            if (appointment.Status != AppointmentStatus.Pending)
            {
                return appointment.Status switch
                {
                    AppointmentStatus.Approved => Error.Failure("This appointment is already approved."),
                    AppointmentStatus.Rejected => Error.Failure("This appointment has been rejected."),
                    AppointmentStatus.Cancelled => Error.Failure("This appointment has been cancelled."),
                    _ => Error.Failure("Invalid appointment status.")
                };
            }
            appointment.Status = AppointmentStatus.Approved;
            _unitOfWork.GetRepository<Appointment, int>().Update(appointment);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");
            return new StateResponse { Message = "Appointment Approved Successfully" };
        }
    }
    #endregion
}
