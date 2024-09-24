using Ardalis.Specification;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Domain.Common.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(BaseSpecification<T> specification);
        Task<bool> AnyAsync(BaseSpecification<T> specification);
        Task<IEnumerable<TResult>> FindAsync<TResult>(BaseSpecification<T, TResult> specification);
    }
}
