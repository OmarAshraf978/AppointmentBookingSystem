using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Commands.CreateAppointment
{
    #region CreateAppointmentCommand
    public class CreateAppointmentCommand : IRequest<Result<CreateAppointmentResponse>>
    {
        public DateTime AppointmentDate { get; set; }
        public int ServiceId { get; set; }
    }
    #endregion
}
