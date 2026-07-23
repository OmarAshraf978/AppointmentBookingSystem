using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Favorites.Commands.AddFavorite
{
    #region AddFavoriteCommand
    public class AddFavoriteCommand : IRequest<Result<FavoriteResponse>>
    {
        public int ServiceId { get; set; }
        public AddFavoriteCommand(int id)
        {
            ServiceId = id;
        }
    }
    #endregion
}
