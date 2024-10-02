using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;


namespace FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses
{
    public record FactoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<MachineResponse> Machines { get; set; }
    }
}
