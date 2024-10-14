using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using FactoryMonitoringSystem.Domain.Shared.Sensor.Enum;

namespace FactoryMonitoringSystem.Domain.Sensors.Entities
{
    public class Sensor : Entity<Guid>
    {
        public string Name { get; set; }
        public SensorEnumType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string Unit { get; set; }
        virtual public List<SensorMachine> SensorMachines { get; set; }

        // Constructor for EF Core
        protected Sensor() { }

        // Business constructor
        public Sensor(Guid id, string name, SensorEnumType type, double minValue,double maxValue, string unit)
        {
            Id = id;
            Type = type;
            Unit = unit;
            Name = name;
            MinValue = minValue; 
            MaxValue = maxValue;
        }

       
    }
}
