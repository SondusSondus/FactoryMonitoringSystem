using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;

namespace FactoryMonitoringSystem.Application.Common.Events
{
    public record ConcurrencyConflictEvent(ConcurrencyConflict ConcurrencyConflict) : NotificationEvent<ConcurrencyConflict>
    {

        public List<NotificationSystemModelEnum> GetNotificationSystems() 
        {
            return base.NotificationSystems = new List<NotificationSystemModelEnum>() 
            { 
                NotificationSystemModelEnum.EmailNotification,
                NotificationSystemModelEnum.InAppNotification,
            };
        }

    }

}
