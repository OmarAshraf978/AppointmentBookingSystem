using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Outputs;
using AutoMapper;
using Domain.Entities.Modules;

namespace Application.Features.Reviews.Mapping
{
    #region ReviewProfile
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<Review, CreateReviewResponse>();
            CreateMap<Review, GetReviewsForServiceResponse>()
                     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));
            CreateMap<Review, GetReviewByIdResponse>()
                     .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));
        }
    }
    #endregion
}
