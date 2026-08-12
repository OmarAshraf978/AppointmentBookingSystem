using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Commands.ApproveServiceProvider;
using Application.Features.ServiceProviders.Commands.CreateServiceProvider;
using Application.Features.ServiceProviders.Commands.DeleteServiceProvider;
using Application.Features.ServiceProviders.Commands.UpdateServiceProvider;
using Application.Features.ServiceProviders.Outputs;
using Application.Features.ServiceProviders.Queries.GetAllServiceProvider;
using Application.Features.ServiceProviders.Queries.GetServiceProviderById;
using Application.Features.ServiceProviders.Queries.GetServiceProviderWithOwnServices;
using FluentValidation.Internal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    #region ServiceProviderController
    public class ServiceProviderController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public ServiceProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpPost("CreateServiceProvider")]
        public async Task<ActionResult<CreateServiceProviderResponse>> CreateServiceProvider(CreateServiceProviderCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpGet("GetServiceProviderWithOwnServices")]
        public async Task<ActionResult<GetServiceProviderWithOwnServicesResponse>> GetServiceProviderWithOwnServices()
        {
            var Result = await _mediator.Send(new GetServiceProviderWithOwnServicesQuery());
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllServiceProviders")]
        public async Task<ActionResult<IEnumerable<GetAllServiceProvidersResponse>>> GetAllServiceProviders()
        {
            var Result = await _mediator.Send(new GetAllServiceProvidersQuery());
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetServiceProviderById/{id}")]
        public async Task<ActionResult<GetServiceProviderByIdResponse>> GetServiceProviderById(int id)
        {
            var Result = await _mediator.Send(new GetServiceProviderByIdQuery(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpPut("UpdateServiceProvider/{id}")]
        public async Task<ActionResult<UpdateServiceProviderResponse>> UpdateServiceProvider(int id, UpdateServiceProviderCommand command)
        {
            command.Id = id;
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin, Service Provider")]
        [HttpPut("DeleteServiceProvider/{id}")]
        public async Task<ActionResult<DeleteServiceProviderResponse>> DeleteServiceProvider(int id)
        {
            var Result = await _mediator.Send(new DeleteServiceProviderCommand(id));
            return HandleResult(Result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ApproveServiceProvider/{id}")]
        public async Task<ActionResult<ApproveServiceProviderResponse>> ApproveServiceProvider(int id)
        {
            var Result = await _mediator.Send(new ApproveServiceProviderCommand(id));
            return HandleResult(Result);
        }
    }
    #endregion
}
