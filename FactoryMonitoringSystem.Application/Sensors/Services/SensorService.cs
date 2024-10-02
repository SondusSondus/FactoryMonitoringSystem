using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using FactoryMonitoringSystem.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Services
{
    public class SensorService : ISensorService , IScopedDependency
    {
        public Task<bool> AddSensorToMachineAsync(Guid machineId, string sensorType, string unit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSensorAsync(Guid sensorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SensorResponse>> GetSensorsByMachineIdAsync(Guid machineId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSensorAsync(Guid sensorId, double newValue)
        {
            throw new NotImplementedException();
        }
    }
}
