using FactoryMonitoringSystem.Shared.Utilities.Enums;
using MediatR;


namespace FactoryMonitoringSystem.Application.Common.Events
{
    public record NotificationEvent<T> : INotification  where T : class
    {
        public T NotificationObject { get; set; }
        public List<NotificationSystemModelEnum> NotificationSystems { get; set; }
    }
}
