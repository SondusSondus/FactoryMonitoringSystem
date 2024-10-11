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
    public class MachineByTypeWithSpecificSensorsSpecification :BaseSpecification<Machine>
    {
        public MachineByTypeWithSpecificSensorsSpecification(string machineType, string sensorType)
        {
            Query.Where(machine => machine.Type.Equals(machineType))
                 .Include(machine => machine.Sensors
                     .Where(sensor => sensor.Type.Equals(sensorType)));  // Include only specific sensor types

        }
    }
}
