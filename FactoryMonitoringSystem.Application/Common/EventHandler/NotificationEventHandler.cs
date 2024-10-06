using FactoryMonitoringSystem.Application.Common.Events;
using FactoryMonitoringSystem.Application.Common.Services;
using FactoryMonitoringSystem.Shared;
using MediatR;
using Microsoft.Extensions.Logging;


namespace FactoryMonitoringSystem.Application.Common.EventHandler
{
    public class NotificationEventHandler<TNotificationEvent> : INotificationHandler<TNotificationEvent> , ITransientDependency
    where TNotificationEvent : NotificationEvent
    {
        private readonly INotificationStrategyResolver _strategyResolver;
        private readonly ILogger<NotificationEventHandler<TNotificationEvent>> _logger;

        public NotificationEventHandler(INotificationStrategyResolver strategyResolver, ILogger<NotificationEventHandler<TNotificationEvent>> logger)
        {
            _strategyResolver = strategyResolver;
            _logger = logger;
        }


        public async Task Handle(TNotificationEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling notification of type {NotificationType}", typeof(TNotificationEvent).Name);


            foreach (var notificationEvent in notification.NotificationSystems)
            {

                var strategy = _strategyResolver.Resolve(notificationEvent);

                if (strategy != null)
                {
                    _logger.LogInformation("Sending notification via {NotificationSystem}", nameof(notificationEvent));

                    await strategy.SendNotificationAsync(notification.NotificationObject, notificationEvent, cancellationToken);
                }
                else
                {
                    _logger.LogError("No strategy found for {NotificationSystem}", nameof(notificationEvent));
                    throw new NotSupportedException("No strategy found to handle this notification.");
                }
            }
        }

    }
}
