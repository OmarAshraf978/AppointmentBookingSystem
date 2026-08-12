using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Commands.UpdateWorkingHour
{
    #region UpdateWorkingHourCommand
    public class UpdateWorkingHourCommand : IRequest<Result<UpdateWorkingHourResponse>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
    #endregion
}
