using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Commands.CreateWorkingHour;
using Application.Features.WorkingHours.Commands.DeleteWorkingHour;
using Application.Features.WorkingHours.Commands.UpdateWorkingHour;
using Application.Features.WorkingHours.Outputs;
using Application.Features.WorkingHours.Queries.GetMyWorkingHour;
using Application.Features.WorkingHours.Queries.GetWorkingHoursByServiceProviderId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    #region WorkingHourController
    public class WorkingHourController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public WorkingHourController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Service Provider")]
        [HttpPost("CreateWorkingHour")]
        public async Task<ActionResult<CreateWorkingHourResponse>> CreateWorkingHour(CreateWorkingHourCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider")]
        [HttpPut("UpdateWorkingHour/{id}")]
        public async Task<ActionResult<UpdateWorkingHourResponse>> UpdateWorkingHour(int id, UpdateWorkingHourCommand command)
        {
            command.Id = id;
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider, Admin")]
        [HttpDelete("DeleteWorkingHour/{id}")]
        public async Task<ActionResult<DeleteWorkingHourResponse>> DeleteWorkingHour(int id)
        {
            var Result = await _mediator.Send(new DeleteWorkingHourCommand(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider")]
        [HttpGet("GetMyWorkingHour")]
        public async Task<ActionResult<IEnumerable<GetMyWorkingHourResponse>>> GetMyWorkingHour()
        {
            var Result = await _mediator.Send(new GetMyWorkingHoursQuery());
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetWorkingHoursByServiceProviderId/{id}")]
        public async Task<ActionResult<IEnumerable<GetWorkingHoursByServiceProviderIdResponse>>> GetWorkingHoursByServiceProviderId(int id)
        {
            var Result = await _mediator.Send(new GetWorkingHoursByServiceProviderIdQuery(id));
            return HandleResult(Result);
        }
    }
    #endregion
}
