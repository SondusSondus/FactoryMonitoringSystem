using FluentValidation;


namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UpdateUser
{
    internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.UpdateUser.Username)
             .NotEmpty().WithMessage(" Name is required.")
             .MaximumLength(100).WithMessage(" name must not exceed 100 characters.");

            RuleFor(command => command.UpdateUser.Email)
             .NotEmpty().WithMessage(" Email is required.")
             .EmailAddress().WithMessage("A valid email is required.");
        }
    }
}


