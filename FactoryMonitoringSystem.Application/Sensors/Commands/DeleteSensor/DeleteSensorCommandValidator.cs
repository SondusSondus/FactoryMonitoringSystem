using FluentValidation;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public class DeleteSensorCommandValidator : AbstractValidator<DeleteSensorCommand>
    {
        public DeleteSensorCommandValidator()
        {
            RuleFor(sensor => sensor.Id)
              .NotEmpty().WithMessage("Sensor Id is required.");
        }
    }
}
