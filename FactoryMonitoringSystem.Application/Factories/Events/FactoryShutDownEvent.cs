using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Events
{
    public class FactoryShutDownEvent :IDomainEvent
    {
        public Guid FactoryId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public FactoryShutDownEvent(Guid factoryId)
        {
            FactoryId = factoryId;
        }
    }
}
