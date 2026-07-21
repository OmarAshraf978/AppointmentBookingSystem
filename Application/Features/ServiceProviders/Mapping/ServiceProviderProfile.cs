using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ServiceProviders.Commands.CreateServiceProvider;
using Application.Features.ServiceProviders.Commands.UpdateServiceProvider;
using Application.Features.ServiceProviders.Outputs;
using AutoMapper;
using Domain.Entities.Modules;

namespace Application.Features.ServiceProviders.Mapping
{
    public class ServiceProviderProfile : Profile
    {
        public ServiceProviderProfile()
        {
            CreateMap<CreateServiceProviderCommand, ServiceProvider>();
            CreateMap<ServiceProvider, CreateServiceProviderResponse>();
            CreateMap<ServiceProvider, GetServiceProviderWithOwnServicesResponse>();
            CreateMap<Service, ServiceResponse>()
                   .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));
            CreateMap<ServiceProvider, GetAllServiceProvidersResponse>()
                   .ForMember(dest => dest.AllServices, opt => opt.MapFrom(src => src.Services));
            CreateMap<ServiceProvider, GetServiceProviderByIdResponse>();
            CreateMap<UpdateServiceProviderCommand, ServiceProvider>();
            CreateMap<ServiceProvider, UpdateServiceProviderResponse>();
        }
    }
}
