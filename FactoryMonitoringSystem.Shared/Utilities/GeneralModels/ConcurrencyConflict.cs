using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralModels
{
    public class ConcurrencyConflict
    {
       
        public EmailModel EmailModel { get; set; }
        public InAppNotificationModel InAppNotificationModel { get; set; }

        public ConcurrencyConflict(string entityName, string userId)
        {
        
            EmailModel = new(userId, $"Concurrency Conflict on {entityName}", $"A concurrency conflict occurred on {entityName}.");
            InAppNotificationModel = new InAppNotificationModel(userId, $"Concurrency conflict detected on {entityName}. Please review the changes.");
        }
    }
}
