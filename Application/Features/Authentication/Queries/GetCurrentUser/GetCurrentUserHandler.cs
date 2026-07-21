using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Outputs;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Queries.GetCurrentUser
{
    #region GetCurrentUserHandler
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetCurrentUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IUserContext userContext)
        {
            _userManager = userManager;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_userContext.UserId))
                return Error.Unauthorized("User.Unauthorized");
            var User = await _userManager.FindByIdAsync(_userContext.UserId);
            if (User is null)
                return Error.NotFound("User.NotFound", "User not found");
            return _mapper.Map<GetCurrentUserResponse>(User);
        }
    }
    #endregion
}
