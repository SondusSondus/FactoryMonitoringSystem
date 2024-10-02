using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Mapster;

namespace FactoryMonitoringSystem.Application.Common.Services
{
    public class InAppNotificationStrategy : INotificationStrategy
    {
        private readonly INotificationService _notificationService;

        public InAppNotificationStrategy(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task SendNotificationAsync<T>(T notification, NotificationSystemModelEnum notificationSystem) where T : class
        {
            if (notificationSystem is NotificationSystemModelEnum.InAppNotification)
            {
                var inAppNotification =  notification.Adapt<InAppNotificationModel>();
                await _notificationService.SendNotificationAsync(inAppNotification);
            }

        }
    }

}
