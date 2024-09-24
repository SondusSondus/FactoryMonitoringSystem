using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Request
{
    public record SensorRequest
    {
        public Guid MachineId { get; set; }
        public string SensorType { get; set; }
        public string Unit { get; set; }
    }
}
