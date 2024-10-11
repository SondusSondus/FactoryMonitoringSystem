using FluentValidation;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public class DeleteSensorCommandValidator : AbstractValidator<DeleteSensorCommand>
    {
        public DeleteSensorCommandValidator()
        {
            RuleFor(sensor => sensor.SensorId)
              .NotEmpty().WithMessage("Sensor Id is required.");
        }
    }
}
