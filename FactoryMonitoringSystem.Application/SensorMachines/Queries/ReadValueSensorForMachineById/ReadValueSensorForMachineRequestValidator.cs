using FactoryMonitoringSystem.Application.SensorMachines.Queries.ReadValueForSensorMachine;
using FluentValidation;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.ReadValueSensorForMachine
{
    public class ReadValueSensorForMachineRequestValidator : AbstractValidator<ReadValueSensorForMachineByIdQuery>
    {
        public ReadValueSensorForMachineRequestValidator()
        {
            RuleFor(query => query.sensorMachineId)
                    .NotEmpty().WithMessage("SensorMachine Id is required."); 
           

        }
    }
}
