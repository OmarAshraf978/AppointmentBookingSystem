using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Outputs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly IIdentityUnitOfWork _unitOfWork;

        public RegisterHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider, IMapper mapper, IIdentityUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var User = _mapper.Map<ApplicationUser>(request);
            var Result = await _userManager.CreateAsync(User, request.Password);
            if(!Result.Succeeded)
            {
                return Result<RegisterResponse>.Fail(Result.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList());
            }
            var token = await _jwtProvider.GenerateTokenAsync(User);
            var address = _mapper.Map<Address>(request);
            address.UserId = User.Id;
            var AddressRepo = _unitOfWork.GetRepository<Address, int>();
            await AddressRepo.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();
            return new RegisterResponse
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                Token = token
            };
        }
    }
}
