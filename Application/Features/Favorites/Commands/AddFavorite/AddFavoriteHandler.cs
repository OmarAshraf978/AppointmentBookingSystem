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

namespace Application.Features.Favorites.Commands.AddFavorite
{
    #region AddFavoriteHandler
    public class AddFavoriteHandler : IRequestHandler<AddFavoriteCommand, Result<FavoriteResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public AddFavoriteHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<FavoriteResponse>> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.GetRepository<Service, int>().GetByIdAsync(request.ServiceId);
            if (service is null)
                return Error.NotFound($"Service With Id: {request.ServiceId} Not Found");
            if (service.IsDeleted)
                return Error.NotFound($"Service With Id: {request.ServiceId} Is Not Available");

            var favoriteExists = await _unitOfWork.GetRepository<Favorite, int>()
                                                  .AnyAsync(x => x.UserId == _userContext.UserId
                                                         && x.ServiceId == request.ServiceId);

            if (favoriteExists)
                return Error.Failure("This Service Is Already In Your Favorites.");

            var favorite = new Favorite
            {
                UserId = _userContext.UserId,
                ServiceId = request.ServiceId
            }; 
            await _unitOfWork.GetRepository<Favorite, int>().AddAsync(favorite);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Failed To Add Favorite.");

            return new FavoriteResponse { Message = "Favorite Added Successfully" };
        }
    }
    #endregion
}
