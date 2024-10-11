using FluentValidation;

namespace FactoryMonitoringSystem.Application.Machines.Commands.CreateMachine
{
    public class CreateMachineCommandValidator : AbstractValidator<CreateMachineCommand>
    {
        public CreateMachineCommandValidator()
        {
            RuleFor(machine => machine.MachineRequest.Name)
                 .NotEmpty().WithMessage("Machine name is required.")
                 .MaximumLength(100).WithMessage("Machine name must not exceed 100 characters.");

            RuleFor(machine => machine.MachineRequest.SerialNumber)
                 .NotEmpty().WithMessage("Serial number is required.");

            RuleFor(machine => machine.MachineRequest.Type)
                 .NotEmpty().WithMessage("Machine type is required.")
                 .IsInEnum().WithMessage("Machine type is enum.");

            RuleFor(machine => machine.MachineRequest.FactoryId)
               .NotEmpty().WithMessage("Factory Id is required.");

        }
    }
}
