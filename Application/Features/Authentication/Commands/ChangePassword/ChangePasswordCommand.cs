using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Commands.ChangePassword
{
    #region ChangePasswordCommand
    public class ChangePasswordCommand : IRequest<Result<ChangePasswordResponse>>
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set;} = null!;
    }
    #endregion
}
