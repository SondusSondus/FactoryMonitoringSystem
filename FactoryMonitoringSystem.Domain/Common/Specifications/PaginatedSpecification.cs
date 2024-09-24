using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Common.Specifications
{
    public class PaginatedSpecification<T> :BaseSpecification<T> where T : class
    {
        // Specification to paginate and sort class by column name
        public PaginatedSpecification(int skip, int take , string ColName)
        {
             Query.Skip(skip)
            .Take(take)
            .OrderBy(factory =>factory.GetType().GetProperty(ColName));

        }
    }
}
