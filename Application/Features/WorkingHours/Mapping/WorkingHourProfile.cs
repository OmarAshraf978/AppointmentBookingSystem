using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Commands.CreateWorkingHour;
using Application.Features.WorkingHours.Commands.UpdateWorkingHour;
using Application.Features.WorkingHours.Outputs;
using AutoMapper;
using Domain.Entities.Modules;

namespace Application.Features.WorkingHours.Mapping
{
    #region WorkingHourProfile
    public class WorkingHourProfile : Profile
    {
        public WorkingHourProfile()
        {
            CreateMap<CreateWorkingHourCommand, WorkingHour>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()));               
            CreateMap<WorkingHour, CreateWorkingHourResponse>()
                .ForMember(dest => dest.ServiceProviderName, opt => opt.MapFrom(src => src.ServiceProvider.BusinessName));
            CreateMap<UpdateWorkingHourCommand, WorkingHour>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()));
            CreateMap<WorkingHour, UpdateWorkingHourResponse>()
                .ForMember(dest => dest.ServiceProviderName, opt => opt.MapFrom(src => src.ServiceProvider.BusinessName));
            CreateMap<WorkingHour, GetMyWorkingHourResponse>()
                .ForMember(dest => dest.ServiceProviderName, opt => opt.MapFrom(src => src.ServiceProvider.BusinessName));
            CreateMap<WorkingHour, GetWorkingHoursByServiceProviderIdResponse>()
                .ForMember(dest => dest.ServiceProviderName, opt => opt.MapFrom(src => src.ServiceProvider.BusinessName));
        }
    }
    #endregion
}
