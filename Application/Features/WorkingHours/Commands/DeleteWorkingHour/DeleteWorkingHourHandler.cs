using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Commands.DeleteWorkingHour
{
    #region DeleteWorkingHourHandler
    public class DeleteWorkingHourHandler : IRequestHandler<DeleteWorkingHourCommand, Result<DeleteWorkingHourResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public DeleteWorkingHourHandler(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }
        public async Task<Result<DeleteWorkingHourResponse>> Handle(DeleteWorkingHourCommand request, CancellationToken cancellationToken)
        {
            var workingHour = await _unitOfWork.GetRepository<WorkingHour, int>()
                                               .GetBySpecificColumnWithIncludeAsync(x => x.Id == request.Id,
                                                                                    x => x.ServiceProvider);
            if (workingHour is null)
                return Error.NotFound($"Working Hour With Id : {request.Id} Not Found");

            var isAdmin = _userContext.Role == "Admin";
            var isServiceProvider = workingHour.ServiceProvider.UserId == _userContext.UserId;
            if (!isAdmin && !isServiceProvider)
                return Error.Unauthorized("You Are Not Authorized To Delete This Working Hour");

            _unitOfWork.GetRepository<WorkingHour, int>().Delete(workingHour);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");

            return new DeleteWorkingHourResponse { Message = "Working Hour Deleted Successfully" };
        }
    }
    #endregion
}
