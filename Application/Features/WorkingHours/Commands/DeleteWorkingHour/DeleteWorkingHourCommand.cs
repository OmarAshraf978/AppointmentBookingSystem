using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Commands.DeleteWorkingHour
{
    #region DeleteWorkingHourCommand
    public class DeleteWorkingHourCommand : IRequest<Result<DeleteWorkingHourResponse>>
    {
        public int Id { get; set; }
        public DeleteWorkingHourCommand(int id)
        {
            Id = id;
        }
    }
    #endregion
}
