﻿

using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Domain.SensorMachine.Entities;
using FactoryMonitoringSystem.Domain.Shared.Sensor.Enum;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses
{
    public record SensorResponse
    {
        public Guid Id { get; set; }
        public Guid MachineId { get; set; }
        public SensorEnumType Type { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        virtual public List<SensorMachineWithMachineResponse> SensorMachines { get; set; }
    }
}
