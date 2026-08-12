using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Commands.CreateWorkingHour
{
    #region CreateWorkingHourCommand
    public class CreateWorkingHourCommand : IRequest<Result<CreateWorkingHourResponse>>
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ServiceProviderId { get; set; }
    }
    #endregion
}
