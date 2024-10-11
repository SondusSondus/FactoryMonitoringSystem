using FluentValidation;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorById
{
    public class GetSensorByIdQueryValidator :AbstractValidator<GetSensorByIdQuery>
    {
        public GetSensorByIdQueryValidator()
        {
            RuleFor(sensor => sensor.SensorId)
                    .NotEmpty().WithMessage("Sensor Id is required.");

        }
    }
}
