using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Favorites.Queries.GetMyFavorites
{
    public class GetMyFavoritesHandler : IRequestHandler<GetMyFavoritesQuery, Result<IEnumerable<GetMyFavoritesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetMyFavoritesHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GetMyFavoritesResponse>>> Handle(GetMyFavoritesQuery request, CancellationToken cancellationToken)
        {
            var favorites = await _unitOfWork.GetRepository<Favorite, int>()
                                             .GetAllBySpecificColumnWithIncludeAsync(x => x.UserId == _userContext.UserId
                                                                                 && !x.Service.IsDeleted,
                                                                                     x => x.Service);
            if (!favorites.Any())
                return Error.NotFound("There Is No Favorites Found For The Current User");

            var response = _mapper.Map<IEnumerable<GetMyFavoritesResponse>>(favorites);
            return Result<IEnumerable<GetMyFavoritesResponse>>.Ok(response);
        }
    }
}
