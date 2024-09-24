using Autofac;
using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Common.Services
{
    public interface INotificationStrategyResolver
    {
        INotificationStrategy Resolve(NotificationSystemModelEnum notification);
    }
    public class NotificationStrategyResolver : INotificationStrategyResolver, IScopedDependency
    {
        private readonly IComponentContext _context;

        private readonly Dictionary<NotificationSystemModelEnum, string> _strategyMap;
        public NotificationStrategyResolver(IComponentContext context)
        {
            _context = context;
            _strategyMap = new Dictionary<NotificationSystemModelEnum, string>
            {
                [NotificationSystemModelEnum.EmailNotification] = nameof(EmailNotificationStrategy),
                [NotificationSystemModelEnum.InAppNotification] = nameof(InAppNotificationStrategy)
            };

        }

        public INotificationStrategy Resolve(NotificationSystemModelEnum notification)
        {
            if (!_strategyMap.TryGetValue(notification, out var nameOfNotificationStrategy))
            {
                throw new NotSupportedException($"The strategy {nameOfNotificationStrategy} not found ");
            }
            return _context.ResolveNamed<INotificationStrategy>(nameOfNotificationStrategy);
        }
    }

}
