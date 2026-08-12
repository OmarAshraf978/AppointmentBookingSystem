using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Commands.CreateService;
using Application.Features.Services.Commands.DeleteService;
using Application.Features.Services.Commands.UpdateService;
using Application.Features.Services.Outputs;
using Application.Features.Services.Queries.GetAllServices;
using Application.Features.Services.Queries.GetServiceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    #region ServiceController
    public class ServiceController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpPost("CreateService")]
        public async Task<ActionResult<CreateServiceResponse>> CreateService(CreateServiceCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllServices")]
        public async Task<ActionResult<IEnumerable<GetAllServicesResponse>>> GetAllServices()
        {
            var Result = await _mediator.Send(new GetAllServicesQuery());
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetServiceById/{id}")]
        public async Task<ActionResult<GetServiceByIdResponse>> GetServiceById(int id)
        {
            var Result = await _mediator.Send(new GetServiceByIdQuery(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider, Admin")]
        [HttpPut("UpdateService/{id}")]
        public async Task<ActionResult<UpdateServiceResponse>> UpdateService(int id, UpdateServiceCommand command)
        {
            command.Id = id;
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Service Provider, Admin")]
        [HttpPut("DeleteService/{id}")]
        public async Task<ActionResult<DeleteServiceResponse>> DeleteService(int id)
        {
            var Result = await _mediator.Send(new DeleteServiceCommand(id));
            return HandleResult(Result);
        }
    }
    #endregion
}
