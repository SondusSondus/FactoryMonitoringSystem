using FluentValidation;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UnlockedUser
{
    internal class UnlockedUserCommandValidator : AbstractValidator<UnlockedUserCommand>
    {
        public UnlockedUserCommandValidator()
        {
            RuleFor(user => user.Id)
                       .NotEmpty().WithMessage("User id is required.");
        }
    }
}
