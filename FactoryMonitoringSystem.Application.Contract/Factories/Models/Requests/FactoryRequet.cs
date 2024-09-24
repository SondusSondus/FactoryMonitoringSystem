using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests
{
    public record FactoryRequet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<MachineRequest> Machines { get; set; }
    }
}
