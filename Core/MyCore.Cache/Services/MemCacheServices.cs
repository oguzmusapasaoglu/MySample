using Microsoft.Extensions.Caching.Memory;

using MyCore.Cache.Interfaces;

namespace MyCore.Common.Services
{
    public class MemCacheServices : IMemCacheServices
    {
        public IMemoryCache _cache;
        public MemCacheServices(IMemoryCache _memoryCache)
        {
            _cache = _memoryCache;
        }
        public void FillCache<TData>(string cacheKey, IEnumerable<TData> data, DateTime? expiredDate = null) where TData : class
        {
            if (_cache.Get(cacheKey).IsNullOrLessOrEqToZero())
                _cache.Remove(cacheKey);
            if (!expiredDate.HasValue)
                expiredDate = DateTime.Now.AddDays(1);

            _cache.Set(cacheKey, data, expiredDate.Value);
        }
        public IQueryable<TData> GetCachedData<TData>(string cacheKey) where TData : class
        {
            IQueryable<TData> returnData;
            _cache.TryGetValue(cacheKey, out returnData);
            return returnData;
        }
        public string GetCacheKey(string baseName)
        {
            return baseName + DateTime.Now.AddDays(1).ToShortDate();
        }
    }
}
