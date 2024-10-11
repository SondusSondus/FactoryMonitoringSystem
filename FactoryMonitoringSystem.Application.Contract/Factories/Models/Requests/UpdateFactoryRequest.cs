


namespace FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests
{
    public record UpdateFactoryRequest : FactoryRequest
    {
        public Guid Id { get; set; }
    }
}
