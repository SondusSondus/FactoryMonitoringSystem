using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Machines.Entities;

namespace FactoryMonitoringSystem.Domain.Machines.Specifications
{
    public class MachineByTypeWithSpecificSensorsSpecification : BaseSpecification<Machine>
    {
        public MachineByTypeWithSpecificSensorsSpecification(string machineType, string sensorType)
        {
            Query.Where(machine => machine.Type.Equals(machineType))
                 .Include(machine => machine.SensorMachine
                     .Where(sensor => sensor.Sensor.Type.Equals(sensorType)));  // Include only specific sensor types

        }
    }
}
