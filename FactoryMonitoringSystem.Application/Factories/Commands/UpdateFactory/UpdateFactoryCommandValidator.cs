using FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactory
{
    internal class UpdateFactoryCommandValidator: AbstractValidator<UpdateFactoryCommand>
    {
        public UpdateFactoryCommandValidator()
        {
            RuleFor(command => command.FactoryRequet.Id)
            .NotEmpty().WithMessage("Factory ID is required.");
            
            RuleFor(command => command.FactoryRequet.Name)
            .NotEmpty().WithMessage("Factory name is required.")
            .MaximumLength(100).WithMessage("Factory name must not exceed 100 characters.");

            RuleFor(command => command.FactoryRequet.Location)
               .NotEmpty().WithMessage("Factory location is required.");
        }
    }
}
