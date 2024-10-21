using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;

namespace FactoryMonitoringSystem.Domain.SensorMachines.Entities
{
    public class SensorMachine : Entity<Guid>
    {
        public Guid SensorId { get; set; }
        public Guid MachineId { get; set; }
        virtual public Machine Machine { get; set; }
        virtual public Sensor Sensor { get; set; }

    }
}
