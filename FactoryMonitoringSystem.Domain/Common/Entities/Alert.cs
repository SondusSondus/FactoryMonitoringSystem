using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Common.Entities
{
    public class Alert
    {
        public Guid Id { get; private set; }
        public Guid SensorId { get; private set; }
        public double Threshold { get; private set; }
        public DateTime TriggeredAt { get; private set; }
        public string Message { get; private set; }

        private Alert()
        {

        }
        public Alert(Guid id, Guid sensorId, double threshold, string message)
        {
            Id = id;
            SensorId = sensorId;
            Threshold = threshold;
            TriggeredAt = DateTime.UtcNow;
            Message = message;
        }
    }
}
