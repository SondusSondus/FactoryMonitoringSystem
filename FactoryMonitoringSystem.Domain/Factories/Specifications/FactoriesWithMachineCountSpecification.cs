using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Shared.Factory.Models;


namespace FactoryMonitoringSystem.Domain.Factories.Specifications
{
    public class FactoriesWithMachineCountSpecification : BaseSpecification<Factory, FactoryWithMachineCountResponse>
    {
        public FactoriesWithMachineCountSpecification()
        {
            Query.Select(factory => new FactoryWithMachineCountResponse
            {
                FactoryId = factory.Id,
                Name = factory.Name,
                MachineCount = factory.Machines.Count  // Project count of machines
            });
        }
    }

}
