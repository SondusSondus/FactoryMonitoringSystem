using ErrorOr;

namespace FactoryMonitoringSystem.Application.Contracts.Common.Services
{
    public interface ICacheService
    {
        void SetCacheAsync(string cacheKey, byte[] cacheEntry);
        void SetCacheAsync(string cacheKey, string cacheEntry);
        void RemoveCacheAsync(string cacheKey);
        byte[] CheckAndGetCacheAsync(string cacheKey);
        string CheckAndGetCacheAsyncForSting(string cacheKey);

    }

}
