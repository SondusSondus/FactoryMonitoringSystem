using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;


namespace FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses
{
    public record MachineResponse
    {
        public Guid Id { get; set; }
        public Guid FactoryId { get; set; }
        public string MachineName { get; set; }
        public string MachineType { get; set; }
        public List<SensorResponse> Sensors { get; set; }
    }
}
