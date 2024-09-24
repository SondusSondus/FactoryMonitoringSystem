using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using System.Reflection.PortableExecutable;
using Machine = FactoryMonitoringSystem.Domain.Machines.Entities.Machine;


namespace FactoryMonitoringSystem.Domain.Machines.Specifications
{
    public class MachineByTypeSpecification :BaseSpecification<Machine>
    {
        // Specification to filter machines by status and include sensors
        public MachineByTypeSpecification(string machineType)
        {
            Query.Where(machine => machine.Type == machineType);
        }
    }
}
