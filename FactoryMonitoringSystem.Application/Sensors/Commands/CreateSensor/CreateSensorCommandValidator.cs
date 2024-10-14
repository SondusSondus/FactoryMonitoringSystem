using FactoryMonitoringSystem.Application.Sensors.Commands.AddSensorToMachine;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.CreateSensor
{
    public class CreateSensorCommandValidator :AbstractValidator<CreateSensorCommand>
    {
        public CreateSensorCommandValidator()
        {
            RuleFor(sensor => sensor.Sensor.Name)
              .NotEmpty().WithMessage("Sensor name is required.")
              .MaximumLength(100).WithMessage("Sensor name must not exceed 100 characters.");

            RuleFor(sensor => sensor.Sensor.MinValue)
                 .NotEmpty().WithMessage("MinValue Id is required.");  
            
            RuleFor(sensor => sensor.Sensor.MaxValue)
                 .NotEmpty().WithMessage("MinValue Id is required.");

            RuleFor(sensor => sensor.Sensor.Unit)
                 .NotEmpty().WithMessage("Unit is required.");

            RuleFor(sensor => sensor.Sensor.Type)
                 .NotEmpty().WithMessage("Sensor type is required.")
                 .IsInEnum().WithMessage("Sensor type is enum.");
        }
    }
}
