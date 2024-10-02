using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace FactoryMonitoringSystem.Domain.Common.Specifications
{
    public class BaseSpecification<T> : Specification<T>, IBaseSpecification<T> where T  : class 
    {
        // Use Ardalis.Specification with Entity Framework Core

        public IQueryable<T> Criteria(IQueryable<T> values,ISpecification<T> specification)
        {
            return SpecificationEvaluator.Default.GetQuery(values, specification);
        }
    }
    public class BaseSpecification<T, TResult> : Specification<T, TResult>, IBaseSpecification<T, TResult> where T  : class 
    {
        // Use Ardalis.Specification with Entity Framework Core

        public IQueryable<TResult> Criteria(IQueryable<T> values, ISpecification<T, TResult> specification)
        {
            return SpecificationEvaluator.Default.GetQuery(values, specification);
        }
    }
}
