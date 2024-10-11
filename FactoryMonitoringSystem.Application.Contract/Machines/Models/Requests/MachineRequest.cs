using FactoryMonitoringSystem.Domain.Shared.Machine.Enum;


namespace FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests
{
    public record MachineRequest
    {
        public string Name { get; set; }
        public MachineTypeEnum Type { get; set; }
        public string SerialNumber { get; set; }
        public Guid FactoryId { get; set; }


    }
}
