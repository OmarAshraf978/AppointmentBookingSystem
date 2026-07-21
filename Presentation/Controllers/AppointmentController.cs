using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Commands.ApproveAppointment;
using Application.Features.Appointments.Commands.CancelAppointment;
using Application.Features.Appointments.Commands.CreateAppointment;
using Application.Features.Appointments.Commands.RejectAppointment;
using Application.Features.Appointments.Outputs;
using Application.Features.Appointments.Queries.GetAppointmentById;
using Application.Features.Appointments.Queries.GetAppointmentsForServiceProvider;
using Application.Features.Appointments.Queries.GetMyAppointments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AppointmentController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "User, Service Provider, Admin")]
        [HttpPost("CreateAppointment")]
        public async Task<ActionResult<CreateAppointmentResponse>> CreateAppointment(CreateAppointmentCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetMyAppointments")]
        public async Task<ActionResult<IEnumerable<GetMyAppointmentsResponse>>> GetMyAppointments()
        {
            var Result = await _mediator.Send(new GetMyAppointmentsQuery());
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider, Admin")]
        [HttpGet("GetAppointmentsForServiceProvider")]
        public async Task<ActionResult<IEnumerable<GetAppointmentsForServiceProviderResponse>>> GetAppointmentsForServiceProvider()
        {
            var Result = await _mediator.Send(new GetAppointmentsForServiceProviderQuery());
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ApproveAppointment/{id}")]
        public async Task<ActionResult<StateResponse>> ApproveAppointment(int id)
        {
            var Result = await _mediator.Send(new ApproveAppointmentCommand(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("RejectAppointment/{id}")]
        public async Task<ActionResult<StateResponse>> RejectAppointment(int id)
        {
            var Result = await _mediator.Send(new RejectAppointmentCommand(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("CancelAppointment/{id}")]
        public async Task<ActionResult<StateResponse>> CancelAppointment(int id)
        {
            var Result = await _mediator.Send(new CancelAppointmentCommand(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider, Admin, User")]
        [HttpGet("GetAppointmentById/{id}")]
        public async Task<ActionResult<GetAppointmentByIdResponse>> GetAppointmentById(int id)
        {
            var Result = await _mediator.Send(new GetAppointmentByIdQuery(id));
            return HandleResult(Result);
        }
    }
}
