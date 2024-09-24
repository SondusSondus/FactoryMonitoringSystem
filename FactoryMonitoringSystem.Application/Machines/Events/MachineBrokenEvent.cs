using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;

namespace FactoryMonitoringSystem.Application.Machines.Events
{
    public class MachineBrokenEvent :IDomainEvent
    {
        public Guid MachineId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public MachineBrokenEvent(Guid machineId)
        {
            MachineId = machineId;
        }
    }
}
