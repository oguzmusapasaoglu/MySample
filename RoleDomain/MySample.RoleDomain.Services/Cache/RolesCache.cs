using MySample.RoleDomain.Services.CacheInterfaces;

using MyCore.Cache.Interfaces;
using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Libraries.Models;

using System.Linq.Expressions;
using MySample.RoleDomain.Repositores.Interfaces;
using Helper.Maps;

namespace MySample.RoleDomain.Services.Cache;
public class RolesCache : IRolesCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "Roles";
    private IRolesRepository repository;
    private IMemCacheServices cache;
    #endregion

    #region Ctor
    public RolesCache(IMemCacheServices _cache, IRolesRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<RolesModel> data)
    {
        var result = cache.GetCachedData<RolesModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(RolesModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<RolesModel> FillData()
    {
        var data = repository.GetAllActive();
        var model = MapperInstance.Instance.Map<IQueryable<RolesEntity>, IQueryable<RolesModel>>(data.Result);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<RolesModel> GetAllData()
    {
        var result = cache.GetCachedData<RolesModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<RolesModel> GetDataByFilter(Expression<Func<RolesModel, bool>> predicate)
    {
        var result = cache.GetCachedData<RolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public RolesModel GetSingleDataByFilter(Expression<Func<RolesModel, bool>> predicate)
    {
        var result = cache.GetCachedData<RolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public RolesModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<RolesModel>(CacheKey);
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
