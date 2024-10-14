using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Factories.Entities;

namespace FactoryMonitoringSystem.Domain.Factories.Specifications
{
    public class FactoryByLocationWithMachinesAndSensorsSpecification : BaseSpecification<Factory>
    {
        // Specification to filter factories by location and include machines with sensors
        public FactoryByLocationWithMachinesAndSensorsSpecification(string location)
        {
            Query.Where(factory => factory.Location == location)
            .Include(factory => factory.Machines)
            .ThenInclude(machine => machine.SensorMachine);  // Include machines and their sensors
        }
    }
}
