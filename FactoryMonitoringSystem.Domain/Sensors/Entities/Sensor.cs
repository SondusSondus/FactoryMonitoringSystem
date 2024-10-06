using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Domain.Machines.Entities;

namespace FactoryMonitoringSystem.Domain.Sensors.Entities
{
    public class Sensor : Entity<Guid>
    {
        public string Type { get; private set; }
        public double Value { get; private set; }
        public string Unit { get; private set; }
        public Guid MachineId { get; set; }

        // Constructor for EF Core
        protected Sensor() { }

        // Business constructor
        public Sensor(Guid id, string type, double value, string unit)
        {
            Id = id;
            Type = type;
            Value = value;
            Unit = unit;
        }

        public void UpdateValue(double newValue)
        {
            // Business rules for updating the sensor value
            Value = newValue;
        }
    }
}
