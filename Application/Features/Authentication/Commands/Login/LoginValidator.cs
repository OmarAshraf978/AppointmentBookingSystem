using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Authentication.Commands.Login
{
    #region LoginValidator
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
    #endregion
}
