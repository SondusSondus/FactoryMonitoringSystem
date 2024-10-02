

namespace FactoryMonitoringSystem.Domain.Common.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}


