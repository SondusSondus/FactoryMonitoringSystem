using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class ReadRepository<T> : IReadRepository<T> where T : class //IRepositoryBase
    {
        private readonly ReadDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IBaseSpecification<T> _baseSpecification;
        public ReadRepository(ReadDbContext context, IBaseSpecification<T> baseSpecification)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _baseSpecification = baseSpecification;

        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {

            return await _dbSet.FindAsync(id, cancellationToken);

        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {

            return await _dbSet.ToListAsync(cancellationToken);

        }    
        public async Task<IEnumerable<T>> GetAllIncludeAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _dbSet;

            // Apply each include expression
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync(cancellationToken);

        }


        // Use Custame Specification
        public async Task<IEnumerable<T>> FindAsync(BaseSpecification<T> specification, CancellationToken cancellationToken)
        {

            var specResult = _baseSpecification.Criteria(_dbSet.AsQueryable(), specification);
            return await specResult.ToListAsync(cancellationToken);
        }
        // Use Ardalis.Specification for projected results (e.g., DTOs)
        public async Task<IEnumerable<TResult>> FindAsync<TResult>(BaseSpecification<T, TResult> specification, CancellationToken cancellationToken)
        {
            var specResult = _baseSpecification.Criteria(_dbSet.AsQueryable(), specification);
            return (IEnumerable<TResult>)await specResult.ToListAsync(cancellationToken);
        }


        public async Task<bool> AnyAsync(BaseSpecification<T> specification, CancellationToken cancellationToken)
        {
            var specResult = _baseSpecification.Criteria(_dbSet.AsQueryable(), specification);
            return await specResult.AnyAsync(cancellationToken);
        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).FirstAsync(cancellationToken);
        }

        public async Task<T> FindIncludeAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Apply each include expression
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<T> FindAsyncIncludeMultiple<TProperty1, TProperty2>(
           CancellationToken cancellationToken,
           Expression<Func<T, bool>> predicate,
           params (Expression<Func<T, IEnumerable<TProperty1>>> include, Expression<Func<TProperty1, TProperty2>>? thenInclude)[] includes)

        {
            IQueryable<T> query = _dbSet;

            // Apply Include and ThenInclude for each pair
            foreach (var (include, thenInclude) in includes)
            {
                if (thenInclude != null)
                {
                    query = query.Include(include).ThenInclude(thenInclude);
                }
                else
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
