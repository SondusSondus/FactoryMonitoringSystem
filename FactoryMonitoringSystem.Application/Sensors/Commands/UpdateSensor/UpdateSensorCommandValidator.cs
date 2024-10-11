using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    public class UpdateSensorCommandValidator : AbstractValidator<UpdateSensorCommand>
    {
        public UpdateSensorCommandValidator()
        {
            RuleFor(sensor => sensor.UpdateSensor.Id)
                .NotEmpty().WithMessage("Sensor Id is required.");

            RuleFor(sensor => sensor.UpdateSensor.Name)
            .NotEmpty().WithMessage("Sensor name is required.")
            .MaximumLength(100).WithMessage("Sensor name must not exceed 100 characters.");

            RuleFor(sensor => sensor.UpdateSensor.MachineId)
                 .NotEmpty().WithMessage("Machine Id is required.");

            RuleFor(sensor => sensor.UpdateSensor.Unit)
                 .NotEmpty().WithMessage("Unit is required.");

            RuleFor(sensor => sensor.UpdateSensor.Type)
                 .NotEmpty().WithMessage("Sensor type is required.")
                 .IsInEnum().WithMessage("Sensor type is enum.");
        }
    }
}
