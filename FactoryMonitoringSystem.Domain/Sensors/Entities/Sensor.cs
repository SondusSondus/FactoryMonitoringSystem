using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Shared.Sensor.Enum;

namespace FactoryMonitoringSystem.Domain.Sensors.Entities
{
    public class Sensor : Entity<Guid>
    {
        public string Name { get; set; }
        public SensorEnumType Type { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public Guid MachineId { get; set; }

        // Constructor for EF Core
        protected Sensor() { }

        // Business constructor
        public Sensor(Guid id, string name, SensorEnumType type, string unit, Guid machineId)
        {
            Id = id;
            Type = type;
            Unit = unit;
            MachineId = machineId;
            Name = name;
        }

        public void UpdateValue(double newValue)
        {
            // Business rules for updating the sensor value
            Value = newValue;
        }
    }
}
