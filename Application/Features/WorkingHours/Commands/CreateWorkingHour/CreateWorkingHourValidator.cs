using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Modules;
using FluentValidation;

namespace Application.Features.WorkingHours.Commands.CreateWorkingHour
{
    #region CreateWorkingHourValidator
    public class CreateWorkingHourValidator : AbstractValidator<CreateWorkingHourCommand>
    {
        public CreateWorkingHourValidator()
        {
            RuleFor(x => x.DayOfWeek).IsInEnum();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty();
            RuleFor(x => x.ServiceProviderId).NotEmpty().GreaterThan(0);
        }
    }
    #endregion
}
