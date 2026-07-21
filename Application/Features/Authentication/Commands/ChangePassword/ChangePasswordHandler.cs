using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.Outputs;
using Domain.Entities.IdentityModule;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<ChangePasswordResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserContext _userContext;

        public ChangePasswordHandler(UserManager<ApplicationUser> userManager, IUserContext userContext)
        {
            _userManager = userManager;
            _userContext = userContext;
        }
        public async Task<Result<ChangePasswordResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_userContext.UserId))
                return Error.Unauthorized("User.Unauthorized");
            var User = await _userManager.FindByIdAsync(_userContext.UserId);
            if (User is null)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var result = await _userManager.ChangePasswordAsync(User, request.OldPassword, request.NewPassword);
            if(!result.Succeeded) 
                return Result<ChangePasswordResponse>.Fail(result.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList());
            return new ChangePasswordResponse
            {
                Message = "Password Changed Successfully"
            };
        }
    }
}
