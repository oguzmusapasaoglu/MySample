using MySample.RoleDomain.Services.CacheInterfaces;

using MyCore.Cache.Interfaces;
using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Libraries.Models;

using System.Linq.Expressions;
using MySample.RoleDomain.Repositores.Interfaces;
using Helper.Maps;

namespace MySample.RoleDomain.Services.Cache;
public class PageObjectCache : IPageObjectCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "PageObject";
    IPageObjectRepository repository;
    IMemCacheServices cache;
    #endregion

    #region Ctor
    public PageObjectCache(IMemCacheServices _cache, IPageObjectRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<PageObjectModel> data)
    {
        var result = cache.GetCachedData<PageObjectModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(PageObjectModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<PageObjectModel> FillData()
    {
        var data = repository.GetAllActive();
        var model = MapperInstance.Instance.Map<IEnumerable<PageObjectEntity>, IEnumerable<PageObjectModel>>(data.Result);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<PageObjectModel> GetAllData()
    {
        var result = cache.GetCachedData<PageObjectModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<PageObjectModel> GetDataByPageId(int? pageID)
    {
        var result = cache.GetCachedData<PageObjectModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(q => q.PageID == pageID);
    }
    public IQueryable<PageObjectModel> GetDataByFilter(Expression<Func<PageObjectModel, bool>> predicate)
    {
        var result = cache.GetCachedData<PageObjectModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public PageObjectModel GetSingleDataByFilter(Expression<Func<PageObjectModel, bool>> predicate)
    {
        var result = cache.GetCachedData<PageObjectModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public PageObjectModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<PageObjectModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(q => q.ID == id);
    }
    public void ReFillCache()
    {
        FillData();
    }
    #endregion
}
