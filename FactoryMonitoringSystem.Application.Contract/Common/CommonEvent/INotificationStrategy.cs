
using FactoryMonitoringSystem.Shared.Utilities.Enums;

namespace FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent
{
    public interface INotificationStrategy
    {
        Task SendNotificationAsync<T>(T notification, NotificationSystemModelEnum notificationSystem) where T : class;
    }
}
