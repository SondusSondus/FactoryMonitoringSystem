using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Services
{
    public interface ISensorService
    {
        Task<bool> AddSensorToMachineAsync(Guid machineId, string sensorType, string unit);
        Task<bool> UpdateSensorAsync(Guid sensorId, double newValue);
        Task<bool> DeleteSensorAsync(Guid sensorId);
        Task<List<SensorResponse>> GetSensorsByMachineIdAsync(Guid machineId);
    }
}
