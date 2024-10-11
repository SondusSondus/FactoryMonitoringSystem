using FactoryMonitoringSystem.Domain.Common.Entities;
using FactoryMonitoringSystem.Domain.Machines.Entities;

namespace FactoryMonitoringSystem.Domain.Factories.Entities
{

    public class Factory : Entity<Guid>
    {
        public string Name { get; set; }
        public string Location { get; set; }
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
    }
}
