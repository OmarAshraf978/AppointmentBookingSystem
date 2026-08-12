using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Commands.AddFavorite;
using Application.Features.Favorites.Commands.RemoveFavorite;
using Application.Features.Favorites.Outputs;
using Application.Features.Favorites.Queries.GetMyFavorites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    #region FavoriteController
    public class FavoriteController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "User")]
        [HttpPost("AddFavorite/{ServiceId}")]
        public async Task<ActionResult<FavoriteResponse>> AddFavorite(int ServiceId)
        {
            var Result = await _mediator.Send(new AddFavoriteCommand(ServiceId));
            return HandleResult(Result);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("RemoveFavorite/{ServiceId}")]
        public async Task<ActionResult<FavoriteResponse>> RemoveFavorite(int ServiceId)
        {
            var Result = await _mediator.Send(new RemoveFavoriteCommand(ServiceId));
            return HandleResult(Result);
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetMyFavorites")]
        public async Task<ActionResult<IEnumerable<GetMyFavoritesResponse>>> GetMyFavorites()
        {
            var Result = await _mediator.Send(new GetMyFavoritesQuery());
            return HandleResult(Result);
        }
    }
    #endregion
}
