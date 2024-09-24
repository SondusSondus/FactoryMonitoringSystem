using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Sensors.ValueObjects
{
    public class SensorReading
    {
        public DateTime Timestamp { get; private set; }
        public double Value { get; private set; }

        public SensorReading(double value)
        {
            Timestamp = DateTime.UtcNow;
            Value = value;
        }
    }
}
