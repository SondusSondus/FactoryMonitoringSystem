using FactoryMonitoringSystem.Shared.Utilities.GeneralServices;
using FluentValidation;


namespace FactoryMonitoringSystem.Application.UserManagement.Commands.VerifyEmail
{
    internal class VerifyEmailCommandValidator: AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            

            RuleFor(reg => reg.VerifyEmailRequest.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("A valid email address is required.")
           .Matches(SystemRegularExpression.Email).WithMessage("A valid email address is required.");

            RuleFor(reg => reg.VerifyEmailRequest.VerificationCode)
                .NotEmpty().WithMessage("Verification Code is required.");
             

        }
    }
}
