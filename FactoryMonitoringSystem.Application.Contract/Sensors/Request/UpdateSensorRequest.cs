using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors
{
    public record UpdateSensorRequest
    {
        public Guid SensorId { get; set; }
        public double NewValue { get; set; }
    }
}
