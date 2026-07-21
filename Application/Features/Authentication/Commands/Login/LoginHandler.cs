using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Outputs;
using Application.Interfaces;
using Domain.Entities.IdentityModule;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Commands.Login
{
    #region LoginHandler
    public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByEmailAsync(request.Email);
            if (User is null)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, request.Password);
            if (!IsPasswordValid)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var token = await _jwtProvider.GenerateTokenAsync(User);
            return new LoginResponse
            {
                Email = User.Email!,
                DisplayName = User.DisplayName,
                Token = token
            };
        }
    }
    #endregion
}
