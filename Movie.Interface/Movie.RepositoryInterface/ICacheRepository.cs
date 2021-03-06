using Movie_store.Common;
using System;

namespace Movie_store.RepositoryInterface
{
    public interface ICacheRepository
    {
        T GetCache<T>(string key);
        void SetCache<T>(string key, T value, int expireHours = CacheConstant.CacheExpireTimeInHours);
    }
}
