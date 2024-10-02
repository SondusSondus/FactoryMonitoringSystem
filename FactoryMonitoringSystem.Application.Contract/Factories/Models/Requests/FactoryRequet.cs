using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;

namespace FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests
{
    public record FactoryRequet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<MachineRequest> Machines { get; set; }
    }
}
