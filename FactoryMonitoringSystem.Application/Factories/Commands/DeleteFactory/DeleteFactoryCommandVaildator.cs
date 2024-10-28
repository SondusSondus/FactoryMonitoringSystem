using FluentValidation;

namespace FactoryMonitoringSystem.Application.Factories.Commands.DeleteFactory
{
    public class DeleteFactoryCommandVaildator : AbstractValidator<DeleteFactoryCommand>
    {
        public DeleteFactoryCommandVaildator()
        {
            RuleFor(command => command.id)
               .NotEmpty().WithMessage("Factory id is required.");
        }
    }
}
