using FactoryMonitoringSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Notifications.Entities
{
    public class Notification : Entity<Guid>
    {
        public string UserEmail { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
