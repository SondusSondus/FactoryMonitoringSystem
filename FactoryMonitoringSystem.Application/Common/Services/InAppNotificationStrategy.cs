using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Notifications.Entities;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;

namespace FactoryMonitoringSystem.Application.Common.Services
{
    public class InAppNotificationStrategy : INotificationStrategy
    {
        private readonly INotificationService _notificationService;

        public InAppNotificationStrategy(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task SendNotificationAsync<T>(T notification) where T : class
        {
            if (notification.GetType().GetProperty("InAppNotificationModel").GetValue(notification) is InAppNotificationModel inAppNotification)
            {
                await _notificationService.SendNotificationAsync(inAppNotification);
            }

        }
    }

}
