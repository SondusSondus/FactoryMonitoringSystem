using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using Serilog;

namespace FactoryMonitoringSystem.Domain.Factories.Entities
{

    public class Factory : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public int NumberOfEmployees { get; set; }

        public List<Machine> Machines { get; private set; } = new List<Machine>();
     

        // Constructor for EF Core
        protected Factory() { }

        // Business constructor
        public Factory(Guid id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
        // Example business logic that triggers a domain event
        public void StartFactory()
        {
            Status = RecordStatus.Active;
           // DomainEvents.Add(new FactoryStartedEvent(Id));
        }


        public void ShutDownFactory()
        {
            Status = RecordStatus.Inactive;
            //DomainEvents.Add(new FactoryShutDownEvent(Id));
        }
        // Add methods to encapsulate business logic, like adding machines
        //public void AddMachine(Machine machine)
        //{
        //    if (machine == null) throw new ArgumentNullException(nameof(machine));
        //    Machines.Add(machine);
        //}
       
    }
}
