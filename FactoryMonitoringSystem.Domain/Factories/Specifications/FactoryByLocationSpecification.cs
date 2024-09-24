using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Domain.Factories.Entities;


namespace FactoryMonitoringSystem.Domain.Factories.Specifications
{
    public class FactoryByLocationSpecification : BaseSpecification<Factory>
    {
        public FactoryByLocationSpecification(string location)
        {
                Query.Where(factory => factory.Location == location);
        }
    }
}
