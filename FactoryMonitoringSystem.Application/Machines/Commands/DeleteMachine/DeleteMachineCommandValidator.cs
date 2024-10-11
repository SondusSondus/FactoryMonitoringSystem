using FluentValidation;

namespace FactoryMonitoringSystem.Application.Machines.Commands.DeleteMachine
{
    public class DeleteMachineCommandValidator :AbstractValidator<DeleteMachineCommand>
    {
        public DeleteMachineCommandValidator()
        {
           RuleFor(machine=>machine.Id)
                   .NotEmpty().WithMessage("Machine id is required.");

        }
    }
}
