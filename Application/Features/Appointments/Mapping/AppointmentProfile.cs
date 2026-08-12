using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Commands.CreateAppointment;
using Application.Features.Appointments.Outputs;
using AutoMapper;
using Domain.Entities.Modules;

namespace Application.Features.Appointments.Mapping
{
    #region AppointmentProfile
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentCommand, Appointment>();
            CreateMap<Appointment, CreateAppointmentResponse>()
                     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
                     .ForMember(dest => dest.ServiceProviderName, opt => opt.MapFrom(src => src.Service.ServiceProvider.BusinessName));
            CreateMap<Appointment, GetMyAppointmentsResponse>()
                     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
                     .ForMember(dest => dest.ServiceProviderName, opt => opt.MapFrom(src => src.Service.ServiceProvider.BusinessName));
            CreateMap<Appointment, GetAppointmentsForServiceProviderResponse>()
                     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));
            CreateMap<Appointment, GetAppointmentByIdResponse>()
                     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
                     .ForMember(dest => dest.ServiceDescription, opt => opt.MapFrom(src => src.Service.Description))
                     .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price))
                     .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.Service.DurationInMinutes))
                     .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Service.ServiceProvider.BusinessName))
                     .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Service.ServiceProvider.Address));
        }
        #endregion
    }
}
