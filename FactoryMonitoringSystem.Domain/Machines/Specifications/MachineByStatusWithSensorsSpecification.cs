using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Machines.Specifications
{
    public class MachineByStatusWithSensorsSpecification: BaseSpecification<Machine>
    {
        public MachineByStatusWithSensorsSpecification(string type)
        {
            Query.Where(machine => machine.Type == type)
                .Include(machine => machine.Sensors);
            
        }
    }
}
