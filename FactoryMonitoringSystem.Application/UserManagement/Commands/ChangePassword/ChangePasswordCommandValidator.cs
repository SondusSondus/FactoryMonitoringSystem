
using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.changePassword.CurrentPassword).NotEmpty().WithMessage("Current password is required.");
            RuleFor(x => x.changePassword.NewPassword).NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters long.");
            RuleFor(x => x.changePassword.ConfirmPassword).Equal(x => x.changePassword.NewPassword).WithMessage("Passwords do not match.");

        }
    }
}
