using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ResetPasswordUser
{
    internal class ResetPasswordUserCommandValidator : AbstractValidator<ResetPasswordUserCommand>
    {
        public ResetPasswordUserCommandValidator()
        {
            RuleFor(user =>user.Id)
                  .NotEmpty().WithMessage("User id is required.");
        }
    }
}
