using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Commands.RejectAppointment
{
    public class RejectAppointmentCommand : IRequest<Result<StateResponse>>
    {
        public int Id { get; set; }
        public RejectAppointmentCommand(int id)
        {
            Id = id;
        }
    }
}
