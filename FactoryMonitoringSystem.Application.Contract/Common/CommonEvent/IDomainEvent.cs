namespace FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
