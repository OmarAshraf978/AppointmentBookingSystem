using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Favorites.Commands.RemoveFavorite
{
    #region RemoveFavoriteCommand
    public class RemoveFavoriteCommand : IRequest<Result<FavoriteResponse>>
    {
        public int ServiceId { get; set; }
        public RemoveFavoriteCommand(int id)
        {
            ServiceId = id;
        }
    }
    #endregion
}
