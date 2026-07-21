using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Commands.ChangePassword;
using Application.Features.Authentication.Commands.Login;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.Outputs;
using Application.Features.Authentication.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<GetCurrentUserResponse>> GetCurrentUser()
        {
            var Result = await _mediator.Send(new GetCurrentUserQuery());
            return HandleResult(Result);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<ActionResult<ChangePasswordResponse>> ChangePassword(ChangePasswordCommand command)
        {
            var Result = await _mediator.Send(command);
            return HandleResult(Result);
        }
    }
}
