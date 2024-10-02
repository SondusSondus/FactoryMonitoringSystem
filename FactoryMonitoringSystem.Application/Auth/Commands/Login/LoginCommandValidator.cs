using FactoryMonitoringSystem.Shared.Utilities.GeneralServices;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(login => login.loginRequest.Email)
             .NotEmpty()
             .NotNull()
             .Matches(SystemRegularExpression.Email)
             .WithMessage("{PropertyName} is not valid");
            
            RuleFor(login => login.loginRequest.Password)
             .NotEmpty()
             .NotNull()
             .WithMessage("{PropertyName} is not valid");
        }
    }
}
