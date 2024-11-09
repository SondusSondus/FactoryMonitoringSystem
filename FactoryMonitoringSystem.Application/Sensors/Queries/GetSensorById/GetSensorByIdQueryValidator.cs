using FluentValidation;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorById
{
    public class GetSensorByIdQueryValidator :AbstractValidator<GetSensorByIdQuery>
    {
        public GetSensorByIdQueryValidator()
        {
            RuleFor(sensor => sensor.Id)
                    .NotEmpty().WithMessage("Sensor Id is required.");

        }
    }
}
