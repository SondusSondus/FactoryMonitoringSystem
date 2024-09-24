using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Sensors.Specifications
{
    public class SensorsAboveThresholdSpecification : BaseSpecification<Sensor>
    {   
        // Specification to filter sensors with values above a threshold

        public SensorsAboveThresholdSpecification(double threshold)
        {
            Query.Where(sensor => sensor.Value > threshold);  // Filter sensors above the threshold


        }
    }
}
