using MySample.RoleDomain.Services.CacheInterfaces;

using MyCore.Cache.Interfaces;
using MySample.RoleDomain.Libraries.Models;

using System.Linq.Expressions;
using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Repositores.Interfaces;

using Helper.Maps;
namespace MySample.RoleDomain.Services.Cache;
public class PagesCache : IPagesCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "Pages";
    private IPagesRepository repository;
    private IMemCacheServices cache;
    #endregion

    #region Ctor
    public PagesCache(IMemCacheServices _cache, IPagesRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<PagesModel> data)
    {
        var result = cache.GetCachedData<PagesModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(PagesModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<PagesModel> FillData()
    {
        var data = repository.GetAllActive();
        var model = MapperInstance.Instance.Map<IEnumerable<PagesEntity>, IEnumerable<PagesModel>>(data.Result);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<PagesModel> GetAllData()
    {
        var result = cache.GetCachedData<PagesModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<PagesModel> GetDataByFilter(Expression<Func<PagesModel, bool>> predicate)
    {
        var result = cache.GetCachedData<PagesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public PagesModel GetSingleDataByFilter(Expression<Func<PagesModel, bool>> predicate)
    {
        var result = cache.GetCachedData<PagesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public PagesModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<PagesModel>(CacheKey);
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
