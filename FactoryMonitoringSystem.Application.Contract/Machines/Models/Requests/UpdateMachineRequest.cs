namespace FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests
{
    public record UpdateMachineRequest : MachineRequest
    {
        public Guid Id { get; set; }
    }
}
