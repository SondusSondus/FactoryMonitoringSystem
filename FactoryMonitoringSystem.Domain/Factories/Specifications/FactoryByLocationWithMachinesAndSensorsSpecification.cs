using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Factories.Specifications
{
    public class FactoryByLocationWithMachinesAndSensorsSpecification : BaseSpecification<Factory>
    {   
        // Specification to filter factories by location and include machines with sensors
        public FactoryByLocationWithMachinesAndSensorsSpecification(string location)
        {
            Query.Where(factory => factory.Location == location)
            .Include(factory => factory.Machines)
            .ThenInclude(machine => machine.Sensors);  // Include machines and their sensors
        }
    }
}
