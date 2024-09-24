using FactoryMonitoringSystem.Domain.Common.Entities;

namespace FactoryMonitoringSystem.Domain.Sensors.Entities
{
    public class Sensor : BaseEntity<Guid>
    {
        public string Type { get; private set; }
        public double Value { get; private set; }
        public string Unit { get; private set; }

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
