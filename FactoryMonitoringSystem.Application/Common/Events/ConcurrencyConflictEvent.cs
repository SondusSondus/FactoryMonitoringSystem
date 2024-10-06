using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;

namespace FactoryMonitoringSystem.Application.Common.Events
{
    public record ConcurrencyConflictEvent(ConcurrencyConflict ConcurrencyConflict) : NotificationEvent
    {
        public override List<NotificationSystemModelEnum> NotificationSystems { get; set; } = new List<NotificationSystemModelEnum>
        {
              NotificationSystemModelEnum.EmailNotification,
              NotificationSystemModelEnum.InAppNotification  
        };
        public override object NotificationObject { get; set; } = ConcurrencyConflict;
    }

}
