using FactoryMonitoringSystem.Application.Common.Events;
using FactoryMonitoringSystem.Application.Common.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.Common.EventHandler
{
    public class NotificationEventHandler<T> : INotificationHandler<NotificationEvent<T>> where T: class 
    {
        private readonly INotificationStrategyResolver _strategyResolver;

        public NotificationEventHandler(INotificationStrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        public async Task Handle(NotificationEvent<T> notification, CancellationToken cancellationToken)
        {
            foreach (var notificationEvent in notification.NotificationSystems)
            {
                var strategy = _strategyResolver.Resolve(notificationEvent);
                if (strategy != null)
                {
                    await strategy.SendNotificationAsync(notification.NotificationObject, notificationEvent);
                }
                else
                {
                    throw new NotSupportedException("No strategy found to handle this notification.");
                }
            }

        }
    }

}
