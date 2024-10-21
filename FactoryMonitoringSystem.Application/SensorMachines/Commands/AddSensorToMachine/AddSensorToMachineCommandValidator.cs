using FluentValidation;

namespace FactoryMonitoringSystem.Application.SensorMachines.Commands.AddSensorToMachine
{
    public class AddSensorToMachineCommandValidator :AbstractValidator<AddSensorToMachineCommand>
    {
        public AddSensorToMachineCommandValidator()
        {
            RuleFor(prop => prop.SensorMachine.MachineId)
               .NotEmpty().WithMessage("Machine Id is required."); 
            RuleFor(prop => prop.SensorMachine.SensorId)
               .NotEmpty().WithMessage("Sensor Id is required.");
        }
    }
}
