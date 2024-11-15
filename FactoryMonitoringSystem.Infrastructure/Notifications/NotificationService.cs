﻿using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Notifications.Entities;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;


namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    public class NotificationService : INotificationService, ITransientDependency
    {
        private readonly IWriteRepository<Notification> _notificationRepository;
        private readonly IHubContext<MonitoringHub> _hubContext;
        private readonly MonitoringSettings _notificationSettings;

        public NotificationService(IWriteRepository<Notification> notificationRepository,
            IHubContext<MonitoringHub> hubContext, IOptions<MonitoringSettings> notificationSettings)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _notificationSettings = notificationSettings.Value;
        }

        // Enqueue notification sending as a background job
        public Task SendNotificationAsync(InAppNotificationModel inAppNotification,CancellationToken cancellationToken)
        {
            if (_notificationSettings.EnableInAppNotifications)
            {
                // Enqueue the notification job for Hangfire
                BackgroundJob.Enqueue(() => ProcessNotification(inAppNotification,cancellationToken));
            }
            return Task.CompletedTask;
        }

        // Process the notification (this is the method Hangfire will call)
        public async Task ProcessNotification(InAppNotificationModel inAppNotification,CancellationToken cancellationToken)
        {
            // Send real-time notification using SignalR
            await _hubContext.Clients.User(inAppNotification.UserEmail).SendAsync("ReceiveNotification", inAppNotification.Message);

            // Save the notification to the database
            var notification = new Notification
            {
                UserEmail = inAppNotification.UserEmail,
                Message = inAppNotification.Message,
                SentAt = DateTime.UtcNow
            };
            _notificationRepository.Add(notification);
            await _notificationRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
