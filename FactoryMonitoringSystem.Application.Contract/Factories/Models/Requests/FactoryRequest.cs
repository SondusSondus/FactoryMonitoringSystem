using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;

namespace FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests
{
    public record FactoryRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
