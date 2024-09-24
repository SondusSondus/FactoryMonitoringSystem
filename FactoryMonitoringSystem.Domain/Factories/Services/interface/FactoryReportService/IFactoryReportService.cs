using FactoryMonitoringSystem.Domain.Factories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Factories.Services
{
    public interface IFactoryReportService
    {
        string GenerateFactoryReport(Factory factory);
        List<string> GenerateReportsForAllFactories(IEnumerable<Factory> factories);
    }
}
