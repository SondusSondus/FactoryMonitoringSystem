using FactoryMonitoringSystem.Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly WriteDbContext _context;
        private readonly DbSet<T> _dbSet;

        public WriteRepository(WriteDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
