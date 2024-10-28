using ErrorOr;

namespace FactoryMonitoringSystem.Application.Contracts.Common.Services
{
    public interface ICacheService
    {
        void SetCacheAsync(string cacheKey, byte[] cacheEntry);
        void RemoveCacheAsync(string cacheKey);
        byte[] CheckAndGetCacheAsync(string cacheKey);

    }

}
