﻿using FactoryMonitoringSystem.Domain.Common.Specifications;
using System.Linq.Expressions;

namespace FactoryMonitoringSystem.Domain.Common.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllIncludeAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate,CancellationToken cancellationToken);
        Task<T> FindIncludeAsync(CancellationToken cancellationToken,Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindAsync(BaseSpecification<T> specification, CancellationToken cancellationToken);
        Task<bool> AnyAsync(BaseSpecification<T> specificatio, CancellationToken cancellationTokenn);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<TResult>> FindAsync<TResult>(BaseSpecification<T, TResult> specification, CancellationToken cancellationToken);
        Task<T> FindAsyncIncludeMultiple<TProperty1, TProperty2>( CancellationToken cancellationToken, Expression<Func<T, bool>> predicate,
                params (Expression<Func<T, IEnumerable<TProperty1>>> include, Expression<Func<TProperty1, TProperty2>>? thenInclude)[] includes);
    }
}
