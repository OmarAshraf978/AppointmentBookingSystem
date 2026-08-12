using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Commands.Register;
using Application.Features.Authentication.Outputs;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Application.Features.Authentication.Mapping
{
    #region AuthenticationProfile
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterCommand, ApplicationUser>().ReverseMap();
            CreateMap<RegisterCommand, Address>().ReverseMap();
            CreateMap<ApplicationUser, GetCurrentUserResponse>();
        }
    }
    #endregion
}
