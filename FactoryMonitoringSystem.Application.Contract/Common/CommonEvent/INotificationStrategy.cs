using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent
{
    public interface INotificationStrategy
    {
        Task SendNotificationAsync<T>(T notification) where T : class;
    }
}
