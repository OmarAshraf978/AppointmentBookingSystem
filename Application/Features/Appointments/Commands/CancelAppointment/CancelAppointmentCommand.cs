using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<Result<StateResponse>>
    {
        public int Id { get; set; }
        public CancelAppointmentCommand(int id)
        {
            Id = id;
        }
    }
}
