using FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactory
{
    public class UpdateFactoryCommandValidator: AbstractValidator<UpdateFactoryCommand>
    {
        public UpdateFactoryCommandValidator()
        {
            RuleFor(command => command.id)
            .NotEmpty().WithMessage("Factory ID is required.");
            
            RuleFor(command => command.factoryRequet.Name)
            .NotEmpty().WithMessage("Factory name is required.")
            .MaximumLength(100).WithMessage("Factory name must not exceed 100 characters.");

            RuleFor(command => command.factoryRequet.Location)
               .NotEmpty().WithMessage("Factory location is required.");
        }
    }
}
