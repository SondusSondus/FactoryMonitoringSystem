﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.CircuitBreaker;
using System.Linq.Expressions;


namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly ReadDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IBaseSpecification<T> _baseSpecification;
        private static AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        public ReadRepository(ReadDbContext context, IBaseSpecification<T> baseSpecification)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _baseSpecification = baseSpecification;
            _circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromMinutes(30)
            );
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await _circuitBreakerPolicy.ExecuteAsync(async () => await _dbSet.FindAsync(id));

            }
            catch (Exception ex)
            {
                // Log error and implement fallback logic or notify a circuit breaker
                HandleFailover(ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _circuitBreakerPolicy.ExecuteAsync(async () => await _dbSet.ToListAsync());
            }
            catch (Exception ex)
            {
                // Log error and implement fallback logic or notify a circuit breaker
                HandleFailover(ex);
                return Enumerable.Empty<T>();  // Gracefully degrade to empty result or cached data
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _circuitBreakerPolicy.ExecuteAsync(async () => await _dbSet.Where(predicate).ToListAsync());
        }

        // Use Custame Specification
        public async Task<IEnumerable<T>> FindAsync(BaseSpecification<T> specification)
        {

            var specResult = _baseSpecification.Criteria(_dbSet.AsQueryable(), specification);
            return await specResult.ToListAsync();
        }
        // Use Ardalis.Specification for projected results (e.g., DTOs)
        public async Task<IEnumerable<TResult>> FindAsync<TResult>(BaseSpecification<T, TResult> specification)
        {
            var specResult = _baseSpecification.Criteria(_dbSet.AsQueryable(), specification);
            return (IEnumerable<TResult>)await specResult.ToListAsync();
        }
        // Custom failover handling logic
        private void HandleFailover(Exception ex)
        {
            // Log the error, notify failover monitoring, or invoke a circuit breaker
            // Add retry or fallback mechanisms
        }

        public async Task<bool> AnyAsync(BaseSpecification<T> specification)
        {
            var specResult = _baseSpecification.Criteria(_dbSet.AsQueryable(), specification);
            return await specResult.AnyAsync();
        }
    }
}
