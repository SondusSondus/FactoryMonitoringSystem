using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FactoryMonitoringSystem.Infrastructure.Cache
{

    public class CacheService : ICacheService, IScopedDependency
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public byte[] CheckAndGetCacheAsync(string cacheKey)
        {
            // Retrieve cache entry if it exists
            return _memoryCache.TryGetValue(cacheKey, out byte[] cacheEntry) ? cacheEntry : null;
        } 
        
        public string CheckAndGetCacheAsyncForSting(string cacheKey)
        {
            // Retrieve cache entry if it exists
            return _memoryCache.TryGetValue(cacheKey, out string cacheEntry) ? cacheEntry : string.Empty;
        }

        public void SetCacheAsync(string cacheKey, byte[] cacheEntry)
        {
           
            _memoryCache.Set(cacheKey, cacheEntry);
        }
        public void SetCacheAsync(string cacheKey, string cacheEntry)
        {
           
            _memoryCache.Set(cacheKey, cacheEntry);
        }

        public void RemoveCacheAsync(string cacheKey)
        {
            // Remove main cache entry
            _memoryCache.Remove(cacheKey);

            // Remove any general key associated with IDs
            if (cacheKey.Contains("id"))
            {
                var generalKey = cacheKey.Split("/id")[0];
                _memoryCache.Remove(generalKey);
            }

        }
    }


}
