using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Factories.Services
{
    public class FactoryReportService : IFactoryReportService, IScopedDependency
    {
        public string GenerateFactoryReport(Factory factory)
        {
            throw new NotImplementedException();
        }

        public List<string> GenerateReportsForAllFactories(IEnumerable<Factory> factories)
        {
            throw new NotImplementedException();
        }
    }
}
