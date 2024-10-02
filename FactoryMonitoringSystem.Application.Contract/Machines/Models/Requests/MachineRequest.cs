using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;


namespace FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests
{
    public record MachineRequest
    {
        public Guid FactoryId { get; set; }
        public string MachineName { get; set; }
        public string MachineType { get; set; }
        public List<SensorRequest> Sensors { get; set; }
    }
}
