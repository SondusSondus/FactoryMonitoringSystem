using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Machines.Entities;

namespace FactoryMonitoringSystem.Domain.Machines.Specifications
{
    public class MachineByStatusWithSensorsSpecification : BaseSpecification<Machine>
    {
        public MachineByStatusWithSensorsSpecification(string type)
        {
            Query.Where(machine => machine.Type.Equals(type))
                .Include(machine => machine.SensorMachine);

        }
    }
}
