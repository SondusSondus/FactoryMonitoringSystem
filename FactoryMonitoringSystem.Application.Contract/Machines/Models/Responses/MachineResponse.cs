using FactoryMonitoringSystem.Application.Contracts.Sensors.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses
{
    public record MachineResponse
    {
        public Guid Id { get; set; }
        public Guid FactoryId { get; set; }
        public string MachineName { get; set; }
        public string MachineType { get; set; }
        public List<SensorResponse> Sensors { get; set; }
    }
}
