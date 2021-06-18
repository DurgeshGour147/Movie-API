using Movie_store.Common;
using Movie_store.RepositoryInterface;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Movie_store.CacheRepository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        public CacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T GetCache<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default;

            return (T)_memoryCache.Get(key);
        }
        public void SetCache<T>(string key, T value, int expireHours = CacheConstant.CacheExpireTimeInHours)
        {
            if (!string.IsNullOrEmpty(key) && value.IsNotNull())
                _memoryCache.Set(key, value, DateTime.Now.AddHours(expireHours));
        }
    }
}
