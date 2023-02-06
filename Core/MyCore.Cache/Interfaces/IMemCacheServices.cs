namespace MyCore.Cache.Interfaces
{
    public interface IMemCacheServices
    {
        void FillCache<TData>(string cacheKey, IEnumerable<TData> data, DateTime? expiredDate = null) where TData : class;
        IQueryable<TData> GetCachedData<TData>(string cacheKey) where TData : class;
        string GetCacheKey(string baseName);
    }
}
