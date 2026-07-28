using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.WorkingHours.Commands.UpdateWorkingHour
{
    #region UpdateWorkingHourValidator
    public class UpdateWorkingHourValidator : AbstractValidator<UpdateWorkingHourCommand>
    {
        public UpdateWorkingHourValidator()
        {
            RuleFor(x => x.DayOfWeek).IsInEnum();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty();
        }
    }
    #endregion
}
