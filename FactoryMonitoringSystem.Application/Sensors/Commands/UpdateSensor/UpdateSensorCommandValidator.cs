using FluentValidation;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    public class UpdateSensorCommandValidator : AbstractValidator<UpdateSensorCommand>
    {
        public UpdateSensorCommandValidator()
        {
            RuleFor(sensor => sensor.id)
                .NotEmpty().WithMessage("Sensor Id is required.");

            RuleFor(sensor => sensor.updateSensor.Name)
            .NotEmpty().WithMessage("Sensor name is required.")
            .MaximumLength(100).WithMessage("Sensor name must not exceed 100 characters.");

            RuleFor(sensor => sensor.updateSensor.MaxValue)
                 .NotEmpty().WithMessage("MaxValue is required.");

            RuleFor(sensor => sensor.updateSensor.MinValue)
                 .NotEmpty().WithMessage("MinValue is required.");

            RuleFor(sensor => sensor.updateSensor.Unit)
                 .NotEmpty().WithMessage("Unit is required.");

            RuleFor(sensor => sensor.updateSensor.Type)
                 .NotEmpty().WithMessage("Sensor type is required.")
                 .IsInEnum().WithMessage("Sensor type is enum.");
        }
    }
}
