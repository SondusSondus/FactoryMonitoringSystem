using FluentValidation;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.GetSensorMachineById
{
    public class GetSensorMachineByIdQueryValidator :AbstractValidator<GetSensorMachineByIdQuery>
    {
        public GetSensorMachineByIdQueryValidator()
        {
            RuleFor(query => query.Id)
                    .NotEmpty().WithMessage("SensorMachine Id is required.");
        }
    }
}
