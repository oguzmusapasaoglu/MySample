using Helper.Maps;

using MyCore.Cache.Interfaces;

using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Repositores.Interfaces;
using MySample.RoleDomain.Services.CacheInterfaces;

using System.Linq.Expressions;

namespace MySample.RoleDomain.Services.Cache;

public class RolePageCache : IRolePageCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "RolePage";
    private IRolePageRepository repository;
    private IMemCacheServices cache;
    #endregion

    #region Ctor
    public RolePageCache(IMemCacheServices _cache, IRolePageRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<RolePageListModel> data)
    {
        var result = cache.GetCachedData<RolePageListModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(RolePageListModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<RolePageListModel> FillData()
    {
        var data = repository.GetAllRolePage().Result.AsEnumerable();
        var model = MapperInstance.Instance.Map<IEnumerable<RolePageEntity>, IEnumerable<RolePageListModel>>(data);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<RolePageListModel> GetAllData()
    {
        var result = cache.GetCachedData<RolePageListModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<RolePageListModel> GetDataByFilter(Expression<Func<RolePageListModel, bool>> predicate)
    {
        var result = cache.GetCachedData<RolePageListModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public RolePageListModel GetSingleDataByFilter(Expression<Func<RolePageListModel, bool>> predicate)
    {
        var result = cache.GetCachedData<RolePageListModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public IQueryable<RolePageListModel> GetDataByRoleId(int roleId)
    {
        var result = cache.GetCachedData<RolePageListModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(q => q.RoleID == roleId);
    }
    public RolePageListModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<RolePageListModel>(CacheKey);
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
