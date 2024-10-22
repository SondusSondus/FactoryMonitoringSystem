using FluentValidation;

namespace FactoryMonitoringSystem.Application.SensorMachines.Commands.AddValueSensorForMachine
{
    public class AddValueSensorForMachineCommandValidator : AbstractValidator<AddValueSensorForMachineCommand>
    {
        public AddValueSensorForMachineCommandValidator()
        {
            RuleFor(command => command.ReadValueSensorForMachine.SensorMachineId)
                .NotEmpty().WithMessage("SensorMachine Id is required.");

            RuleFor(command => command.ReadValueSensorForMachine.Value)
                .NotEmpty().WithMessage("Value is required.");
        }
    }
}
