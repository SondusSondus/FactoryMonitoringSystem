using FactoryMonitoringSystem.Shared.Utilities.GeneralServices;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(reg => reg.email)
              .NotEmpty().WithMessage("Email is required.")
              .EmailAddress().WithMessage("A valid email address is required.")
              .Matches(SystemRegularExpression.Email).WithMessage("A valid email address is required.");
        }
    }
}
