using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Services.Commands.CreateService;
using Application.Features.Services.Commands.UpdateService;
using Application.Features.Services.Outputs;
using AutoMapper;
using Domain.Entities.Modules;

namespace Application.Features.Services.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceCommand, Service>()
                  .ForMember(dest => dest.Name, otp => otp.MapFrom(src => src.ServiceName));
            CreateMap<Service, CreateServiceResponse>()
                  .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Service, GetAllServicesResponse>()
                  .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Service, GetServiceByIdResponse>()
                  .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Service, UpdateServiceResponse>()
                  .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdateServiceCommand, Service>()
                  .ForMember(dest => dest.Name, otp => otp.MapFrom(src => src.ServiceName));
        }
    }
}
