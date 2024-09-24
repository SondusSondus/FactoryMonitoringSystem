using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Responses
{
    public record SensorResponse
    {
        public Guid Id { get; set; }
        public Guid MachineId { get; set; }
        public string SensorType { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }
    }
}
