using FactoryMonitoringSystem.Shared.Utilities.GeneralServices;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ConfirmPassword
{
    public class ConfirmPasswordCommandValidator : AbstractValidator<ConfirmPasswordCommand>
    {
        public ConfirmPasswordCommandValidator()
        {


            RuleFor(x => x.confirmPassword.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("A valid email address is required.")
           .Matches(SystemRegularExpression.Email).WithMessage("A valid email address is required.");

            RuleFor(x => x.confirmPassword.VerificationCode)
                .NotEmpty().WithMessage("Verification Code is required.");

            RuleFor(x => x.confirmPassword.NewPassword).NotEmpty().WithMessage("New password is required.")
              .MinimumLength(8).WithMessage("New password must be at least 8 characters long.");

            RuleFor(x => x.confirmPassword.ConfirmPassword).Equal(x => x.confirmPassword.NewPassword).WithMessage("Passwords do not match.");


        }
    }
}
