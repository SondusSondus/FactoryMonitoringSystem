using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;


namespace FactoryMonitoringSystem.Application.Contracts.Common.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(InAppNotificationModel inAppNotification, CancellationToken cancellationToken);
    }
}
