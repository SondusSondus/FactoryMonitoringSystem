using FluentValidation;


namespace FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory
{
    public class CreateFactoryCommandValidator : AbstractValidator<CreateFactoryCommand>
    {
        public CreateFactoryCommandValidator()
        {
            RuleFor(command => command.FactoryRequet.Name)
             .NotEmpty().WithMessage("Factory name is required.")
             .MaximumLength(100).WithMessage("Factory name must not exceed 100 characters.");

            RuleFor(command => command.FactoryRequet.Location)
               .NotEmpty().WithMessage("Factory location is required.");
        }
    }
}
