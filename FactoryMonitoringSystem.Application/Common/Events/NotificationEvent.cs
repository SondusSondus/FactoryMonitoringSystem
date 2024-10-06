using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using MediatR;


namespace FactoryMonitoringSystem.Application.Common.Events
{
    public abstract record NotificationEvent: INotification  
    {
        public abstract object NotificationObject { get; set; }
        public abstract List<NotificationSystemModelEnum> NotificationSystems { get; set; }
    }
}
