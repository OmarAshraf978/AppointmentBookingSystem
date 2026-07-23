using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Outputs;
using AutoMapper;
using Domain.Entities.Modules;

namespace Application.Features.Favorites.Mapping
{
    #region FavoriteProfile
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, GetMyFavoritesResponse>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name));
        }
    }
    #endregion
}
