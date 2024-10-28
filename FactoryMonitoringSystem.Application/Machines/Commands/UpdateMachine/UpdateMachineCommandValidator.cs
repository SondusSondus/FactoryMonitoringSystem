using FluentValidation;

namespace FactoryMonitoringSystem.Application.Machines.Commands.UpdateMachine
{
    public class UpdateMachineCommandValidator :AbstractValidator<UpdateMachineCommand>
    {
        public UpdateMachineCommandValidator()
        {
            RuleFor(machine => machine.id)
                 .NotEmpty().WithMessage("Machine id is required.");
            
            RuleFor(machine => machine.updateMachine.Name)
                 .NotEmpty().WithMessage("Machine name is required.")
                 .MaximumLength(100).WithMessage("Machine name must not exceed 100 characters.");

            RuleFor(machine => machine.updateMachine.SerialNumber)
                 .NotEmpty().WithMessage("Serial number is required.");

            RuleFor(machine => machine.updateMachine.Type)
                 .NotEmpty().WithMessage("Machine type is required.")
                 .IsInEnum().WithMessage("Machine type is enum.");

            RuleFor(machine => machine.updateMachine.FactoryId)
              .NotEmpty().WithMessage("Factory Id is required.");
        }
    }
}
