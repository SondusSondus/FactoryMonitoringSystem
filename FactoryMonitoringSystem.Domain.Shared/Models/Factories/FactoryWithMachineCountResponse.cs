using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Shared
{
    public record FactoryWithMachineCountResponse
    {
        public Guid FactoryId { get; set; }
        public string Name { get; set; }
        public int MachineCount { get; set; }
    }
}
