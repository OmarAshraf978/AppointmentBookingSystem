using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Favorites.Outputs;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Favorites.Commands.RemoveFavorite
{
    public class RemoveFavoriteHandler : IRequestHandler<RemoveFavoriteCommand, Result<FavoriteResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public RemoveFavoriteHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }
        public async Task<Result<FavoriteResponse>> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _unitOfWork.GetRepository<Favorite, int>()
                                            .GetBySpecificColumnAsync(x => x.ServiceId == request.ServiceId
                                                                   && x.UserId == _userContext.UserId);
            if (favorite is null)
                return Error.NotFound($"Favorite with ServiceId {request.ServiceId} Is Not In Your Favorites");

            _unitOfWork.GetRepository<Favorite, int>().Delete(favorite);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Failed To Remove Favorite");

            return new FavoriteResponse { Message = "Favorite Removed Successfully" };
        }
    }
}
