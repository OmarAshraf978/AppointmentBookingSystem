using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.AppointmentDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(x => x.ServiceId).NotEmpty().GreaterThan(0);
        }
    }
}
