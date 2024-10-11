using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FactoryMonitoringSystem.Domain.Shared.Machine.Enum;

namespace FactoryMonitoringSystem.Domain.Machines.Entities
{
    public class Machine : Entity<Guid>
    {
        public string Name { get; set; }
        public MachineTypeEnum Type { get; set; }
        public string SerialNumber { get; set; }
        public Guid FactoryId { get; set; }
        public List<Sensor> Sensors { get; private set; } = new List<Sensor>();


        // Constructor for EF Core
        protected Machine() { }

        // Business constructor
        public Machine(Guid id, string name, MachineTypeEnum type, string serialNumber, Guid factoryId)
        {
            Id = id;
            Name = name;
            Type = type;
            SerialNumber = serialNumber;
            FactoryId = factoryId;
        }

    }
}
