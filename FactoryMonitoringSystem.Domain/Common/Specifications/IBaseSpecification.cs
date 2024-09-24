using Ardalis.Specification;


namespace FactoryMonitoringSystem.Domain.Common.Specifications
{
    public interface IBaseSpecification<T> : ISpecification<T> where T : class
    {
        public IQueryable<T> Criteria(IQueryable<T> values, ISpecification<T> specification);
    } 
    public interface IBaseSpecification<T, TResult>: ISpecification<T, TResult> where T : class
    {
        public IQueryable<TResult> Criteria(IQueryable<T> values, ISpecification<T, TResult> specification);
    }
}
