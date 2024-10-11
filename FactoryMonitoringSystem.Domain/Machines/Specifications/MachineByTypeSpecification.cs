using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Shared.Sensor.Enum;
using Machine = FactoryMonitoringSystem.Domain.Machines.Entities.Machine;


namespace FactoryMonitoringSystem.Domain.Machines.Specifications
{
    public class MachineByTypeSpecification : BaseSpecification<Machine>
    {
        // Specification to filter machines by status and include sensors
        public MachineByTypeSpecification(SensorEnumType machineType)
        {
            Query.Where(machine => machine.Type.Equals(machineType));
        }
    }
}
