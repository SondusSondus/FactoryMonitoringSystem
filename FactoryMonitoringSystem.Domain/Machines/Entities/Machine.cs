using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FactoryMonitoringSystem.Shared.Utilities.Enums;

namespace FactoryMonitoringSystem.Domain.Machines.Entities
{
    public class Machine : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string SerialNumber { get; set; }
        public Guid FactoryId { get; set; }
        public List<Sensor> Sensors { get; private set; } = new List<Sensor>();


        // Constructor for EF Core
        protected Machine() { }

        // Business constructor
        public Machine(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
        // Example domain logic that triggers an event
        public void MarkAsBroken()
        {
            Status = RecordStatus.Inactive;
            //DomainEvents.Add(new MachineBrokenEvent(Id));
        }
        public void AddSensor(Sensor sensor)
        {
            if (sensor == null) throw new ArgumentNullException(nameof(sensor));
            Sensors.Add(sensor);
        }
    }
}
