using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Commands.Login
{
    #region LoginCommand
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    #endregion
}
