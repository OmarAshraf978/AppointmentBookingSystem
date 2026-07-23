using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Favorites.Queries.GetMyFavorites
{
    #region GetMyFavoritesQuery
    public class GetMyFavoritesQuery : IRequest<Result<IEnumerable<GetMyFavoritesResponse>>>
    {
    }
    #endregion
}
