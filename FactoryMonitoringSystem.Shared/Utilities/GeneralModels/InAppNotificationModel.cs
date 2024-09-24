using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralModels
{
    public class InAppNotificationModel
    {
        public string UserId { get; set; }
        public string Message { get; set; }

        public InAppNotificationModel(string userId, string message)
        {
            Message= message;
            UserId= userId;

        }
    }
}
