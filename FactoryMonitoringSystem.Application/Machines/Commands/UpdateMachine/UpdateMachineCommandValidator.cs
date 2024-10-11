using FluentValidation;

namespace FactoryMonitoringSystem.Application.Machines.Commands.UpdateMachine
{
    public class UpdateMachineCommandValidator :AbstractValidator<UpdateMachineCommand>
    {
        public UpdateMachineCommandValidator()
        {
            RuleFor(machine => machine.UpdateMachine.Id)
                 .NotEmpty().WithMessage("Machine id is required.");
            
            RuleFor(machine => machine.UpdateMachine.Name)
                 .NotEmpty().WithMessage("Machine name is required.")
                 .MaximumLength(100).WithMessage("Machine name must not exceed 100 characters.");

            RuleFor(machine => machine.UpdateMachine.SerialNumber)
                 .NotEmpty().WithMessage("Serial number is required.");

            RuleFor(machine => machine.UpdateMachine.Type)
                 .NotEmpty().WithMessage("Machine type is required.")
                 .IsInEnum().WithMessage("Machine type is enum.");

            RuleFor(machine => machine.UpdateMachine.FactoryId)
              .NotEmpty().WithMessage("Factory Id is required.");
        }
    }
}
