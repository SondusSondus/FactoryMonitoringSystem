using FactoryMonitoringSystem.Shared.Utilities.GeneralServices;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.RegistrationUsers
{
    public class RegistrationUserCommandValidator : AbstractValidator<RegistrationUserCommand>
    {
        public RegistrationUserCommandValidator()
        {
            RuleFor(reg => reg.SingUpRequest.Username)
             .NotEmpty().WithMessage("Username is required.")
             .MinimumLength(4).WithMessage("Username must be at least 4 characters long.")
             .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(reg => reg.SingUpRequest.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("A valid email address is required.")
           .Matches(SystemRegularExpression.Email).WithMessage("A valid email address is required.");

            RuleFor(reg => reg.SingUpRequest.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(reg => reg.SingUpRequest.ConfirmPassword)
               .Equal(reg => reg.SingUpRequest.Password).WithMessage("Passwords do not match.");
        }
    }
}
